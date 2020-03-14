import http from "./httpService";

const apiEndPoint = "banners";

async function getBanners() {
  return await http.get(apiEndPoint);
}

export default {
  getBanners
};
