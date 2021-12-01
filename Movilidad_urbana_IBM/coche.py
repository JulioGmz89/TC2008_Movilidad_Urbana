from mesa import Agent, Model
from mesa.time import RandomActivation
from mesa.datacollection import DataCollector

# matplotlib lo usaremos crear una animación de cada uno de los pasos del modelo.
#%matplox|tlib inline
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.animation as animation
plt.rcParams["animation.html"] = "jshtml"
matplotlib.rcParams['animation.embed_limit'] = 2**128

import numpy as np
import pandas as pd
class Control(Agent):
    def __init__(self, unique_id, model):
        super().__init__(unique_id, model)
        self.model = model
        
        self.contar()
        self.cambio_luz()
        self.timer = 10
        
    def step(self):
        self.contar()
        
        if self.timer == 0:
            self.cambio_luz()
            self.timer = 10
            
        self.timer -= 1
            
        
    def cambio_luz(self):
        if(self.cont_norte == max(self.cont_norte,max(self.cont_sur,max(self.cont_este,self.cont_oeste)))):
            self.model.norte.light = True
            self.model.sur.light = False
            self.model.este.light = False
            self.model.oeste.light = False
        elif(self.cont_sur == max(self.cont_norte,max(self.cont_sur,max(self.cont_este,self.cont_oeste)))):
            self.model.norte.light = False
            self.model.sur.light = True
            self.model.este.light = False
            self.model.oeste.light = False
        elif(self.cont_este == max(self.cont_norte,max(self.cont_sur,max(self.cont_este,self.cont_oeste)))):
            self.model.norte.light = False
            self.model.sur.light = False
            self.model.este.light = True
            self.model.oeste.light = False
        elif(self.cont_oeste == max(self.cont_norte,max(self.cont_sur,max(self.cont_este,self.cont_oeste)))):
            self.model.norte.light = False
            self.model.sur.light = False
            self.model.este.light = False
            self.model.oeste.light = True
        
    
    def contar(self):
        self.cont_norte = 0
        self.cont_sur = 0
        self.cont_este = 0
        self.cont_oeste = 0
        for agent in self.model.schedule.agents:
            if agent.orientation == 'up-down':
                self.cont_norte += 1
            elif agent.orientation == 'down-up':
                self.cont_sur += 1
            elif agent.orientation == 'right-left':
                self.cont_este += 1
            elif agent.orientation == 'left-right':
                self.cont_oeste += 1
                
class Semaforo(Agent):
    def __init__(self, unique_id, model):
        super().__init__(unique_id, model)
        
        self.light = False

class Coche(Agent):
    def __init__(self, unique_id, model, x, y, orientation):
        super().__init__(unique_id, model)
        self.model = model
        
        # Posición del coche
        self.position = np.array((x,y), dtype=np.float64)
        
        # Orientación del coche
        self.orientation = orientation
        
        # Vector de velocidad y aceleración del coche
        self.check_speed()
        
        # Máxima distancia entre coches
        self.MAX_DIST = 0.8
        
        #variables para la vuelta
        self.index = 0
        self.turn = False
        
    def step(self):
        self.check_speed()
        
        if self.stop() == False:
            self.check_turn()
            if self.turn == True:
                if self.index < len(self.curve):
                    self.position = self.curve[self.index]
                    self.index += 1
                else:
                    self.turn = False
                    self.index = 0
            elif self.turn == False:
                self.position = self.position + self.velocity
            

    def check_speed(self):
        # Horizontal de derecha a izquierda
        if self.orientation == 'right-left':
            vel = -1
            #acc = vel/2
            self.velocity = np.array((vel, 0), dtype=np.float64)
            #self.acceleration = np.array((acc, 0), dtype=np.float64)
            
        # Vertical de arriba hacia abajo
        elif self.orientation == 'up-down':
            vel = -1
            #acc = vel/2
            self.velocity = np.array((0, vel), dtype=np.float64)
            #self.acceleration = np.array((0, acc), dtype=np.float64)
        
        # Horizontal de izquierda a derecha
        elif self.orientation == 'left-right':
            vel = 1
            #acc = vel/2
            self.velocity = np.array((vel, 0), dtype=np.float64)
            #self.acceleration = np.array((acc, 0), dtype=np.float64)
            
        # Vertical de abajo hacia arriba
        elif self.orientation == 'down-up':
            vel = 1
            #acc = vel/2
            self.velocity = np.array((0, vel), dtype=np.float64)
            #self.acceleration = np.array((0, acc), dtype=np.float64)
    
    def stop(self):
        if self.orientation == 'right-left' and self.position.flatten()[0] == 3: 
            if self.model.este.light == False:
                return True

        elif self.orientation == 'up-down' and self.position.flatten()[1] == 3:
            if self.model.norte.light == False:
                return True

        elif self.orientation == 'left-right' and self.position.flatten()[0] == -3:
            if self.model.oeste.light == False:
                return True

        elif self.orientation == 'down-up' and self.position.flatten()[1] == -3:
            if self.model.sur.light == False:
                return True
        
        if self.check_cars() == True:
            return True
        
        return False
    
    def check_cars(self):
        for agent in self.model.schedule.agents:

            if self.orientation == 'right-left':
                # Carril 1
                if self.position[1] == 0:
                    if self.position[0] - 1 == agent.position[0] and self.position[1] - agent.position[1] == 0:
                        return True
                # Carril 2
                elif self.position[1] == 2:
                    if self.position[0] - 1 == agent.position[0] and self.position[1] - agent.position[1] == 0:
                        return True
            
            elif self.orientation == 'left-right':
                # Carril 1
                if self.position[1] == 0:
                    if self.position[0] + 1 == agent.position[0] and self.position[1] - agent.position[1] == 0:
                        return True
                # Carril 2
                elif self.position[1] == -2:
                    if self.position[0] + 1 == agent.position[0] and self.position[1] - agent.position[1] == 0:
                        return True
    
            elif self.orientation == 'up-down':
                # Carril 1
                if self.position[0] == 0:
                    if self.position[1] - 1 == agent.position[1] and self.position[0] - agent.position[0] == 0:
                        return True
                # Carril 2
                elif self.position[0] == -2:
                    if self.position[1] - 1 == agent.position[1] and self.position[0] - agent.position[0] == 0:
                        return True
            
            elif self.orientation == 'down-up':
                # Carril 1
                if self.position[0] == 0:
                    if self.position[1] + 1 == agent.position[1] and self.position[0] - agent.position[0] == 0:
                        return True
                # Carril 2
                elif self.position[0] == 2:
                    if self.position[1] + 1 == agent.position[1] and self.position[0] - agent.position[0] == 0:
                        return True
            
        return False
    
    
    def check_turn(self):
        choice = np.random.choice([True,False])
        
        # Vuelta a la derecha
        if self.orientation == 'right-left' and self.position.flatten()[0] == 3 and self.position.flatten()[1] == 2: 
            if choice == True:
                self.curve = np.array([[2.68,2.05], [2.4,2.17], [2.2,2.39], [2.06,2.67]])
                self.turn = True
                self.orientation = 'down-up'
            
        elif self.orientation == 'down-up' and self.position.flatten()[0] == 2 and self.position.flatten()[1] == -3: 
            if choice == True:
                self.curve = np.array([[2.05,-2.7], [2.22,-2.42], [2.45,-2.23], [2.72,-2.06]])
                self.turn = True
                self.orientation = 'left-right'
            
        elif self.orientation == 'left-right' and self.position.flatten()[0] == -3 and self.position.flatten()[1] == -2: 
            if choice == True:
                self.curve = np.array([[-2.7,-2.08], [-2.46,-2.26], [-2.26,-2.47], [-2.11,-2.73]])
                self.turn = True
                self.orientation = 'up-down'
            
        elif self.orientation == 'up-down' and self.position.flatten()[0] == -2 and self.position.flatten()[1] == 3: 
            if choice == True:
                self.curve = np.array([[-2.05,2.7], [-2.22,2.42], [-2.45,2.23], [-2.72,2.06]])
                self.turn = True
                self.orientation = 'right-left'
        
        # Vuelta izquierda
        if self.orientation == 'right-left' and self.position.flatten()[0] == 3 and self.position.flatten()[1] == 0:
            self.curve = np.array([[2.38,-0.24], [1.74,-0.5], [1.01,-0.8], [0.32,-1.21], [-0.27,-1.65], [-0.81,-2.09], [-1.41,-2.54]])
            self.turn = True
            self.orientation = 'up-down'
                
        elif self.orientation == 'up-down' and self.position.flatten()[0] == 0 and self.position.flatten()[1] == 3:
            self.curve = np.array([[0.21,2.39], [0.53,1.78], [0.83,1.15], [1.19,0.42], [1.7,-0.29], [2.05,-0.93], [2.5,-1.5]])
            self.turn = True
            self.orientation = 'left-right'
                
        elif self.orientation == 'left-right' and self.position.flatten()[0] == -3 and self.position.flatten()[1] == 0:
            self.curve = np.array([[-2.38,0.24], [-1.74,0.5], [-1.01,0.8], [-0.32,1.21], [0.27,1.65], [0.81,2.09], [1.41,2.54]])
            self.turn = True
            self.orientation = 'down-up'
                
        elif self.orientation == 'down-up' and self.position.flatten()[0] == 0 and self.position.flatten()[1] == -3:
            self.curve = np.array([[-0.21,-2.39], [-0.53,-1.78], [-0.83,-1.15], [-1.19,-0.42], [-1.7,0.29], [-2.05,0.93], [-2.5,1.5]])
            self.turn = True
            self.orientation = 'right-left'

def get_cars(model):
    result = []
    for agent in model.schedule.agents:
        result.append(agent.position)
    result = np.asarray(result)
    return result

def get_lights(model):
    result = []
    for agent in model.lights.agents:
        result.append(agent.light)
    result = np.asarray(result)
    return result

class CarModel(Model):
    def __init__(self, N):
        self.num_agents = N
        self.schedule = RandomActivation(self)
        self.lights = RandomActivation(self)
        
        self.iniciar_semaforos()
        self.control = Control(1, self)
        
        self.id = 8
        
        self.source = np.array([[10, 0], [10, 2], [0, 10], [-2, 10], [-10, 0], [-10, -2], [0, -10], [2, -10]])
        temp = []
        i = 0
        
        g = np.random.randint(8)
        while i in range(g):
            pos = np.random.randint(8)
            
            if pos in temp:
                continue
            
            x = self.source[pos][0]
            y = self.source[pos][1]
            orientation = self.initial_orientation(pos)
            car = Coche(i, self, x, y, orientation)
            self.schedule.add(car)
            temp.append(pos)
            i += 1

            
        self.datacollector = DataCollector(model_reporters = {"Coches" : get_cars, "Luces" : get_lights})
            
    def step(self):
        if self.id < self.num_agents:
            pos = np.random.randint(8)
            x = self.source[pos][0]
            y = self.source[pos][1]
            orientation = self.initial_orientation(pos)
            car = Coche(self.id, self, x, y, orientation)
            self.schedule.add(car)
            self.id += 1
        
        self.datacollector.collect(self)
        self.schedule.step()
        self.control.step()
        
    def initial_orientation(self, pos):
        if 0 <= pos <= 1:
            return 'right-left'
        elif 2 <= pos <= 3:
            return 'up-down'
        elif 4 <= pos <= 5:
            return 'left-right'
        elif 6 <= pos <= 7:
            return 'down-up'
        
    def iniciar_semaforos(self):
        self.norte = Semaforo(1, self)
        self.sur = Semaforo(2, self)
        self.este = Semaforo(3, self)
        self.oeste = Semaforo(4, self)
        
        self.lights.add(self.norte)
        self.lights.add(self.sur)
        self.lights.add(self.este)
        self.lights.add(self.oeste)
        