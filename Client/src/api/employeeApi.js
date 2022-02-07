import axiosClient from './axiosClient';

const url = '/employee/';

const employeeApi = {
  getAll: async (pageNumber, pageSize) => {
    const res = await axiosClient.get(`${url}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
    return res.data;
  },

  getById: async idEmployee => {
    const res = await axiosClient.get(`${url}${idEmployee}`);
    return res.data;
  },

  create: async employee => {
    const res = await axiosClient.post(url, employee);
    return res.data;
  },

  update: async employee => {
    const res = await axiosClient.put(`${url}${employee.id}`, employee);
    return res;
  },

  delete: async idEmployee => {
    const res = await axiosClient.delete(`${url}${idEmployee}`);
    return res.data;
  },
};

export default employeeApi;
