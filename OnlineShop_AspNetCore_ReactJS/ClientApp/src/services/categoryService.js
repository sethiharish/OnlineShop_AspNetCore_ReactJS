import http from "./httpService";

const apiEndPoint = "categories";

async function getCategories() {
  return await http.get(apiEndPoint);
}

export default {
  getCategories
};
