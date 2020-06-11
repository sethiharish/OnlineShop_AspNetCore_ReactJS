import http from "./httpService";

const apiEndPoint = "pies";

async function getPiesOfTheWeek() {
  return await http.get(`${apiEndPoint}?isPieOfTheWeek=true`);
}

async function getPies() {
  return await http.get(apiEndPoint);
}

async function getPie(pieId) {
  return await http.get(`${apiEndPoint}/${pieId}`);
}

export default {
  getPiesOfTheWeek,
  getPies,
  getPie,
};
