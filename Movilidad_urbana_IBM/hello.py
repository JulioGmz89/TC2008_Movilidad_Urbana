from flask import Flask, render_template, request, jsonify
import json, logging, os, atexit

from coche import Control, Semaforo, Coche, CarModel

app = Flask(__name__, static_url_path='')

# On IBM Cloud Cloud Foundry, get the port number from the environment variable PORT
# When running this app on the local machine, default the port to 8000
port = int(os.getenv('PORT', 8000))

@app.route('/')
def root():
    #MAX_GENERATIONS = 100
    #N = 20

    #model = CarModel(N)

    #for i in range(MAX_GENERATIONS):
        #model.step()
        #var = model.datacollector.get_model_vars_dataframe()
    return jsonify([{"message","var"}])

if __name__ == '__main__': 
    app.run(host='0.0.0.0', port=port, debug=True)