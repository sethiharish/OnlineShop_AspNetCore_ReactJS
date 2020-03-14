import http from "./httpService";

const apiEndPoint = "workitems";

async function getWorkItems() {
  return await http.get(apiEndPoint);
}

export default {
  getWorkItems
};
