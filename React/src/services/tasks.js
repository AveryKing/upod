import axios from 'axios'
const baseUrl = 'https://localhost:7135/api/tasks'

let token = null

const setToken = newToken => {
    token = `bearer ${newToken}`
}

const getAll = (token) => {
    axios.defaults.headers.common = {'Authorization': `bearer ${token}`}
    const request = axios.get(baseUrl)
    return request.then(response => response.data)
}

const create = async newObject => {
    const config = {
        headers: { Authorization: token },
    }

    const response = await axios.post(baseUrl, newObject, config)
    return response.data
}

const update = (id, newObject) => {
    const request = axios.put(`${ baseUrl }/${id}`, newObject)
    return request.then(response => response.data)
}

export default { getAll, create, update, setToken }