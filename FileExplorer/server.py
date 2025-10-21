from flask import Flask, jsonify, make_response
app = Flask(__name__)
@app.route('/hello', methods=['GET'])
def hello():
 data = {
 "message": "Hello from the server!",
 "status": "success"
 }
 response = make_response(jsonify(data), 200)
 response.headers['X-Custom-Header'] = 'MyCustomValue'
 return response
if __name__ == '__main__':
 app.run(debug=True)
