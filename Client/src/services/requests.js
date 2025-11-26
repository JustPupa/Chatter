import api from "./api";

export const getLatestPosts = async () => {
    try {
        const response = await api.get("/SMPost/latest");
        console.log("Latest 100 posts:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPostsByUserId = async (userid) => {
    try {
        const response = await api.get(`/SMPost/${userid}/posts`);
        console.log("User posts:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getReactionsByPostId = async (postid) => {
    try {
        const response = await api.get(`/Emoji/${postid}/reactions`);
        console.log("Post reactions:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getLikesByPostId = async (postid) => {
    try {
        const response = await api.get(`/SMPost/${postid}/likes`);
        console.log("Post likes:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getEmojis = async (emojisPage, emojisCount) => {
    try {
        const response = await api.get("/Emoji", {
        params: {
            pageNumber: emojisPage,
            pageSize: emojisCount
        }
        });
        console.log("List of all emojis:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getSubscribersByUserId = async (userid) => {
    try {
        const response = await api.get(`/User/${userid}/subscribers`);
        console.log("User subscribers:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPfpicsByUserId = async (userid) => {
    try {
        const response = await api.get(`/ProfilePicture/${userid}/pfpictures`);
        console.log("User profile pictures links:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getChatsByUserId = async (userid, chatsPage, chatsCount) => {
    try {
        const response = await api.get(`/Chat/${userid}/chats`, {
        params: {
            pageNumber: chatsPage,
            pageSize: chatsCount
        }
        });
        console.log("User chats:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getUsersByChatId = async (chatid) => {
    try {
        const response = await api.get(`/Chat/${chatid}/users`);
        console.log("Chat users:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getMessagesByChatId = async (chatid) => {
    try {
        const response = await api.get(`/Message/${chatid}/messages`);
        console.log("Chat messages:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPinnedMessagesByChatId = async (chatid) => {
    try {
        const response = await api.get(`/Message/${chatid}/pinnedmessages`);
        console.log("Chat pinned messages:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}