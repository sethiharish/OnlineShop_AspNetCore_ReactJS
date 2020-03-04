import http from "./httpService";
import config from "../config.json";

const apiEndPoint = `${config.baseApi}/pies`;

async function getPiesOfTheWeek() {
  let results = { data: null, error: null };

  try {
    const { data } = await http.get(`${apiEndPoint}?isPieOfTheWeek=true`);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function getPies() {
  let results = { data: null, error: null };

  try {
    const { data } = await http.get(apiEndPoint);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

async function getPie(pieId) {
  let results = { data: null, error: null };

  try {
    const { data } = await http.get(`${apiEndPoint}/${pieId}`);
    results.data = data;
  } catch (error) {
    results.error = error;
  }

  return results;
}

export default {
  getPiesOfTheWeek,
  getPies,
  getPie
};
