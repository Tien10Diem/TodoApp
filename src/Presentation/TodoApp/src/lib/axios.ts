import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5121', // URL của backend
  headers: {
    'Content-Type': 'application/json',
  },
});

export default api;
