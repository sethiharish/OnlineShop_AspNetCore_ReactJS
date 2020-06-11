import http from "./httpService";

const apiEndPoint = "iterations";

async function getIterations() {
  return await http.get(apiEndPoint);
}

export default {
  getIterations,
};
