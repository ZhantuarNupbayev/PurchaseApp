import axios from 'axios';

class AuthService {
    API_URL = process.env.VUE_APP_API_URL;
  login(username: string, password: string) {
    return axios
      .post(this.API_URL + '/authentication/login', {
        username,
        password
      })
      .then(response => {
        if (response.data.token) {
          localStorage.setItem('user', JSON.stringify(response.data));
        }
        
        return response.data;
      });
  }

  logout() {
    localStorage.removeItem('user');
  }

  register(firstname: string, lastname:string, username: string, email: string, password: string) {
    return axios.post(this.API_URL + '/authentication', {
      firstname,
      lastname,
      username,
      email,
      password,
      roles: [
        "User"
      ]
    });
  }

  resetPassword(username: string, oldPassword: string, password: string, confirmPassword: string) {
    console.log(username, oldPassword, password, confirmPassword);
    return axios.post(this.API_URL + '/authentication/resetPassword', {
      username,
      oldPassword,
      password,
      confirmPassword
    });
  }
}

export default new AuthService();

