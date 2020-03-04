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

export default {
  getPiesOfTheWeek
};
