export const BASE_URL = 'https://localhost:7001/api'; // Adjust port to match your backend API

export const ApiEndpoints = {
  auth: {
    login: `${BASE_URL}/auth/login`,
    register: `${BASE_URL}/auth/register`
  },
  dashboard: `${BASE_URL}/dashboard`,
  projects: {
    list: `${BASE_URL}/projects`,
    detail: (id: number | string) => `${BASE_URL}/projects/${id}`,
    create: `${BASE_URL}/projects`,
    update: (id: number | string) => `${BASE_URL}/projects/${id}`,
    delete: (id: number | string) => `${BASE_URL}/projects/${id}`
  },
  tasks: {
    list: `${BASE_URL}/tasks`,
    detail: (id: number | string) => `${BASE_URL}/tasks/${id}`,
    create: `${BASE_URL}/tasks`,
    update: (id: number | string) => `${BASE_URL}/tasks/${id}`,
    delete: (id: number | string) => `${BASE_URL}/tasks/${id}`
  }
};