import axiosClient from './axiosClient';

const authApi = {
  login: async user => {
    return await axiosClient.post('auth/login', user);
  },
  resiger: async user => {
    return await axiosClient.post('auth/register', user);
  },
  getUser: async idUser => {
    return await axiosClient.get(`user/${idUser}`);
  },
};
export default authApi;
