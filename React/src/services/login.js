import axios from 'axios'
const baseUrl = 'https://localhost:7135/api/users/login'

export default async function login(credentials)  {
    const response = await axios.post(baseUrl, credentials)
    return response.data
}

