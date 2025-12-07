export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: '/Auth/Login', // POST /api/Auth/login
    REGISTER: '/Auth/Register', // POST /api/Auth/register
  },

  USERS: {
    GET_ME: '/Users/me', // GET /api/Users/me
    GET_ALL: '/Users', // GET /api/Users
    GET_BY_ID: (id: number) => `/Users/${id}`, // GET /api/Users/{id}
  },

  TASKS: {
    GET_ALL: '/Tasks', // GET /api/Tasks
    GET_BY_ID: (id: number) => `/Tasks/${id}`, // GET /api/Tasks/{id}
    CREATE: '/Tasks', // POST /api/Tasks
    UPDATE: (id: number) => `/Tasks/${id}`, // PUT /api/Tasks/{id}
    DELETE: (id: number) => `/Tasks/${id}`, // DELETE /api/Tasks/{id}
  },
};
