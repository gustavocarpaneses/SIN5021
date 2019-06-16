import gym
from collections import deque
import numpy as np
import random
import math

class QLearning():

    def __init__(self, buckets=(5, 5, 24, 10,)):
        
        self.env = gym.make("CartPole-v0")
    
        self.buckets = buckets

        self.alpha = 0.1
        self.gamma = 0.99
        self.epsilon = 0.1

        self.q_table = np.zeros(self.buckets + (self.env.action_space.n,))

    def discretize(self, obs):
        upper_bounds = [self.env.observation_space.high[0], 2.5, self.env.observation_space.high[2], 5]
        lower_bounds = [self.env.observation_space.low[0], -2.5, self.env.observation_space.low[2], -5]
        ratios = [(obs[i] + abs(lower_bounds[i])) / (upper_bounds[i] - lower_bounds[i]) for i in range(len(obs))]
        new_obs = [int(round((self.buckets[i] - 1) * ratios[i])) for i in range(len(obs))]
        new_obs = [min(self.buckets[i] - 1, max(0, new_obs[i])) for i in range(len(obs))]
        return tuple(new_obs)        

    def run(self):

        scores = deque(maxlen=100)

        for e in range(0, 100000):

            s = self.discretize(self.env.reset())

            rTotal = 0
            iterations = 0
            done = False

            while not done:
                if random.uniform(0, 1) < self.epsilon:
                    a = self.env.action_space.sample() #exploration
                else:
                    a = np.argmax(self.q_table[s]) #exploitation

                obs, r, done, info = self.env.step(a)

                next_s = self.discretize(obs)

                delta = r + self.gamma * np.max(self.q_table[next_s]) - self.q_table[s][a]

                self.q_table[s][a] += self.alpha * delta

                s = next_s

                #contar iterações (passos)
                #calcular recompensa média por passos
                #recompensa acumulada

                rTotal += r
                iterations += 1

            scores.append(iterations)
            mean_score = np.mean(scores)

            if mean_score >= 195.0 and e > 200:
                print(f"Resolveu em {e} episódios")
                return

            if e % 100 == 0:
                print(f"Episódio: {e}, Recompensa acumulada: {rTotal}, Recompensa Média (100 últimos): {mean_score}, Cart Position: {obs[0]}, Cart Velocity: {obs[1]}, Pole Angle: {(obs[2]*360)/(2*3.14)}, Pole Velocity: {obs[3]}")

        print(f"Não conseguiu resolver, mesmo depois de {e} episódios")
        return

if __name__ == "__main__":
    algorithm = QLearning()
    algorithm.run()                