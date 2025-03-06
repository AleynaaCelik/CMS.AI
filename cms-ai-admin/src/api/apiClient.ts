import axios from 'axios';

const baseURL = process.env.REACT_APP_API_URL || 'https://localhost:7226/api';

const apiClient = axios.create({
  baseURL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export default apiClient;