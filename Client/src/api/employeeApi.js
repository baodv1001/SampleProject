import axiosClient from './axiosClient';

const url = '/employee/';

const employeeApi = {
  getAll: async () => {
    const res = await axiosClient.get(url);
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
    const res = await axiosClient.put(`${url}${employee.idEmployee}`, employee);
    return res.data;
  },

  delete: async idEmployee => {
    const res = await axiosClient.delete(`${url}${idEmployee}`);
    return res.data;
  },
};

export default employeeApi;
