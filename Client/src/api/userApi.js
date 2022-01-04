import axiosClient from './axiosClient';

const url = '/user/';

const itemApi = {
  getAll: async () => {
    const res = await axiosClient.get(url);
    return res.data;
  },

  getById: async idUser => {
    const res = await axiosClient.get(`${url}${idUser}`);
    return res.data;
  },

  update: async user => {
    const res = await axiosClient.put(`${url}${user.idUser}`, user);
    return res.data;
  },

  voting: async voting => {
    const res = await axiosClient.post(`${url}voting`, voting);
    return res.data;
  },
};

export default itemApi;
