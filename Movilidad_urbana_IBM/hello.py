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

from flask import Flask, render_template, request, jsonify
import json, logging, os, atexit

from coche import Control, Semaforo, Coche, CarModel

MAX_GENERATIONS = 100
N = 20

model = CarModel(N)

for i in range(MAX_GENERATIONS):
    model.step()

var = model.datacollector.get_model_vars_dataframe()

print(var.iloc[0][0])
""""
app = Flask(__name__, static_url_path='')

# On IBM Cloud Cloud Foundry, get the port number from the environment variable PORT
# When running this app on the local machine, default the port to 8000
port = int(os.getenv('PORT', 8000))

@app.route('/')
def root():
    MAX_GENERATIONS = 100
    N = 20

    model = CarModel(N)

    for i in range(MAX_GENERATIONS):
        model.step()
        var = model.datacollector.get_model_vars_dataframe()
    return jsonify([{var}])

if __name__ == '__main__': 
    app.run(host='0.0.0.0', port=port, debug=True)
"""