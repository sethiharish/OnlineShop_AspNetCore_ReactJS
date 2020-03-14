import axios from "axios";
import config from "../config.json";

async function get(apiEndPoint) {
  let results = { data: null, error: null };

  try {
    const { data } = await axios.get(`${config.baseApi}/${apiEndPoint}`);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function post(apiEndPoint, obj) {
  let results = { data: null, error: null };

  try {
    const { data } = await axios.post(`${config.baseApi}/${apiEndPoint}`, obj);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function put(apiEndPoint, obj) {
  let results = { data: null, error: null };

  try {
    const { data } = await axios.put(`${config.baseApi}/${apiEndPoint}`, obj);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function remove(apiEndPoint) {
  let results = { data: null, error: null };

  try {
    const { data } = await axios.delete(`${config.baseApi}/${apiEndPoint}`);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

export default {
  get: get,
  post: post,
  put: put,
  delete: remove
};
