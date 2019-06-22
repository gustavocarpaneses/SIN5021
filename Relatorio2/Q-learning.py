import gym
from collections import deque
import numpy as np
import random
import math
import matplotlib.pyplot as plt

class QLearning():

    def __init__(self, buckets=(1, 1, 6, 12,)):
        
        self.env = gym.make("CartPole-v0")
    
        self.buckets = buckets

        self.min_alpha = 0.1
        self.min_epsilon = 0.1
        self.gamma = 0.9

        self.q_table = np.zeros(self.buckets + (self.env.action_space.n,))

    def discretize(self, obs):
        upper_bounds = [self.env.observation_space.high[0], 0.5, self.env.observation_space.high[2], math.radians(50)]
        lower_bounds = [self.env.observation_space.low[0], -0.5, self.env.observation_space.low[2], -math.radians(50)]
        ratios = [(obs[i] + abs(lower_bounds[i])) / (upper_bounds[i] - lower_bounds[i]) for i in range(len(obs))]
        new_obs = [int(round((self.buckets[i] - 1) * ratios[i])) for i in range(len(obs))]
        new_obs = [min(self.buckets[i] - 1, max(0, new_obs[i])) for i in range(len(obs))]
        return tuple(new_obs)       

    def get_epsilon(self, t):
        return max(self.min_epsilon, min(1, 1.0 - math.log10((t + 1) / 10)))

    def get_alpha(self, t):
        return max(self.min_alpha, min(1.0, 1.0 - math.log10((t + 1) / 25)))     

    def run(self):

        scores = deque(maxlen=100)

        numsteps = []
        avg_numsteps = []

        for e in range(0, 5000):

            s = self.discretize(self.env.reset())

            alpha = self.get_alpha(e)
            epsilon = self.get_epsilon(e)

            rTotal = 0
            iterations = 0
            done = False

            while not done:
                if random.uniform(0, 1) < epsilon:
                    a = self.env.action_space.sample() #exploration
                else:
                    a = np.argmax(self.q_table[s]) #exploitation

                #self.env.render()
                obs, r, done, _ = self.env.step(a)
                next_s = self.discretize(obs)

                delta = r + self.gamma * np.max(self.q_table[next_s]) - self.q_table[s][a]
                self.q_table[s][a] += alpha * delta
                s = next_s

                rTotal += r
                iterations += 1

            scores.append(iterations)
            mean_score = np.mean(scores)

            numsteps.append(iterations)
            avg_numsteps.append(np.mean(numsteps[-100:]))

            #if mean_score >= 195.0:
            #    print(f"Resolveu em {e} episódios")
            #    return
            if e % 100 == 0:
                print(f"Episódio: {e}, Recompensa acumulada: {rTotal}, Recompensa Média (100 últimos): {mean_score}, Epsilon: {epsilon}, Alpha: {alpha}")

        plt.plot(numsteps)
        plt.plot(avg_numsteps)
        plt.xlabel('Episode')
        plt.show()

        #print(f"Não conseguiu resolver, mesmo depois de {e} episódios")
        return

if __name__ == "__main__":
    algorithm = QLearning()
    algorithm.run()                