import axios from 'axios';

export const getLatestPosts = async () => {
    try {
        const response = await axios.get("https://localhost:7052/api/SMPost/latest")
        console.log("Latest 100 posts:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPostsByUserId = async (userid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/SMPost/${userid}/posts`)
        console.log("User posts:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getReactionsByPostId = async (postid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/Emoji/${postid}/reactions`)
        console.log("Post reactions:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getLikesByPostId = async (postid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/SMPost/${postid}/likes`)
        console.log("Post likes:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getEmojis = async () => {
    try {
        const response = await axios.get("https://localhost:7052/api/Emoji")
        console.log("List of all emojis:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getSubscribersByUserId = async (userid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/User/${userid}/subscribers`)
        console.log("User subscribers:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPfpicsByUserId = async (userid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/User/${userid}/pfpicture`)
        console.log("User profile pictures links:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getChatsByUserId = async (userid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/Chat/${userid}/chats`)
        console.log("User chats:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getUsersByChatId = async (chatid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/Chat/${chatid}/users`)
        console.log("Chat users:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getMessagesByChatId = async (chatid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/Chat/${chatid}/messages`)
        console.log("Chat messages:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPinnedMessagesByChatId = async (chatid) => {
    try {
        const response = await axios.get(`https://localhost:7052/api/Chat/${chatid}/pinnedmessages`)
        console.log("Chat pinned messages:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}