import { api } from "./api";

export const getLatestPosts = async () => {
    try {
        const response = await api.get("/SMPost/latest");
        console.log("Latest 100 posts:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPostsByUserId = async (userid, postsPage, postsCount) => {
    try {
        const response = await api.get(`/SMPost/${userid}/posts`, {
            params: {
                pageNumber: postsPage,
                pageSize: postsCount
            }
        });
        console.log("User posts:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getReactionsByPostId = async (postid, reactionsPage, reactionsCount) => {
    try {
        const response = await api.get(`/Emoji/${postid}/reactions`, {
            params: {
                pageNumber: reactionsPage,
                pageSize: reactionsCount
            }
        });
        console.log("Post reactions:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getLikesByPostId = async (postid, likesPage, likesCount) => {
    try {
        const response = await api.get(`/SMPost/${postid}/likes`, {
            params: {
                pageNumber: likesPage,
                pageSize: likesCount
            }
        });
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

export const getSubscribersByUserId = async (userid, subscribersPage, subscribersCount) => {
    try {
        const response = await api.get(`/User/${userid}/subscribers`, {
            params: {
                pageNumber: subscribersPage,
                pageSize: subscribersCount
            }
        });
        console.log("User subscribers:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPfpicsByUserId = async (userid, pfpPage, pfpCount) => {
    try {
        const response = await api.get(`/ProfilePicture/${userid}/pfpictures`, {
            params: {
                pageNumber: pfpPage,
                pageSize: pfpCount
            }
        });
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

export const getUsersByChatId = async (chatid, chatsPage, chatsCount) => {
    try {
        const response = await api.get(`/Chat/${chatid}/users`, {
            params: {
                pageNumber: chatsPage,
                pageSize: chatsCount
            }
        });
        console.log("Chat users:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getMessagesByChatId = async (chatid, messagePage, messageCount) => {
    try {
        const response = await api.get(`/Message/${chatid}/messages`, {
            params: {
                pageNumber: messagePage,
                pageSize: messageCount
            }
        });
        console.log("Chat messages:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}

export const getPinnedMessagesByChatId = async (chatid, pinPage, pinCount) => {
    try {
        const response = await api.get(`/Message/${chatid}/pinnedmessages`, {
            params: {
                pageNumber: pinPage,
                pageSize: pinCount
            }
        });
        console.log("Chat pinned messages:");
        console.log(response.data);
    } catch(e) {
        console.error(e);
    }
}