import { useState } from 'react';
import '../../styles/test.css';
import { 
  getLatestPosts,
  getPostsByUserId,
  getReactionsByPostId,
  getLikesByPostId,
  getEmojis,
  getSubscribersByUserId,
  getPfpicsByUserId,
  getChatsByUserId,
  getUsersByChatId,
  getMessagesByChatId,
  getPinnedMessagesByChatId
} from '../../services/requests';

function Test() {
  const [userIdForPosts, setUserIdForPosts] = useState(1);
  const [userPostsPage, setUserPostsPage] = useState(1);
  const [userPostsCount, setUserPostsCount] = useState(15);

  const [postIdForReactions, setPostIdForReactions] = useState(2);
  const [reactionsPage, setReactionsPage] = useState(1);
  const [reactionsCount, setReactionsCount] = useState(15);

  const [postIdForLikes, setPostIdForLikes] = useState(3);
  const [likesPage, setLikesPage] = useState(1);
  const [likesCount, setLikesCount] = useState(15);

  const [emojiPage, setEmojiPage] = useState(1);
  const [emojiCount, setEmojiCount] = useState(10);

  const [userIdForSubscribers, setUserIdForSubscribers] = useState(1);
  const [subscribersPage, setSubscribersPage] = useState(1);
  const [subscribersCount, setSubscribersCount] = useState(15); 

  const [userIdForPfpictures, setUserIdForPfpictures] = useState(2);
  const [pfpPage, setPfpPage] = useState(1);
  const [pfpCount, setPfpCount] = useState(15); 

  const [userIdForChats, setUserIdForChats] = useState(2);
  const [userChatsPage, setUserChatsPage] = useState(1);
  const [userChatsCount, setUserChatsCount] = useState(15);

  const [chatIdForUsers, setChatIdForUsers] = useState(1);
  const [chatsPage, setChatsPage] = useState(1);
  const [chatsCount, setChatsCount] = useState(15);

  const [chatIdForMessages, setChatIdForMessages] = useState(1);
  const [messagePage, setMessagePage] = useState(1);
  const [messageCount, setMessageCount] = useState(15);

  const [chatIdForPinMessages, setChatIdForPinMessages] = useState(2);
  const [pinPage, setPinPage] = useState(1);
  const [pinCount, setPinCount] = useState(15);

  return (
    <>
      <div>
        <p>
          Get latest posts
          <button onClick={() => getLatestPosts()}>
            Execute
          </button>
        </p>

        <p>
          <input type="number" value={userIdForPosts} onChange={(e) => setUserIdForPosts(e.currentTarget.value)}></input>
          <input type="number" value={userPostsPage} onChange={(e) => setUserPostsPage(e.currentTarget.value)}></input>
          <input type="number" value={userPostsCount} onChange={(e) => setUserPostsCount(e.currentTarget.value)}></input>
          <button onClick={() => getPostsByUserId(userIdForPosts, userPostsPage, userPostsCount)}>
            Get user posts by User ID
          </button>
        </p>

        <p>
          <input type="number" value={postIdForReactions} onChange={(e) => setPostIdForReactions(e.currentTarget.value)}></input>
          <input type="number" value={reactionsPage} onChange={(e) => setReactionsPage(e.currentTarget.value)}></input>
          <input type="number" value={reactionsCount} onChange={(e) => setReactionsCount(e.currentTarget.value)}></input>
          <button onClick={() => getReactionsByPostId(postIdForReactions, reactionsPage, reactionsCount)}>
            Get reactions by Post ID
          </button>
        </p>

        <p>
          <input type="number" value={postIdForLikes} onChange={(e) => setPostIdForLikes(e.currentTarget.value)}></input>
          <input type="number" value={likesPage} onChange={(e) => setLikesPage(e.currentTarget.value)}></input>
          <input type="number" value={likesCount} onChange={(e) => setLikesCount(e.currentTarget.value)}></input>
          <button onClick={() => getLikesByPostId(postIdForLikes, likesPage, likesCount)}>
            Get likes by Post ID
          </button>
        </p>

        <p>
          <input type="number" value={emojiPage} onChange={(e) => setEmojiPage(e.currentTarget.value)}></input>
          <input type="number" value={emojiCount} onChange={(e) => setEmojiCount(e.currentTarget.value)}></input>
          <button onClick={() => getEmojis(emojiPage, emojiCount)}>
            Get Emoji list
          </button>
        </p>

        <p>
          <input type="number" value={userIdForSubscribers} onChange={(e) => setUserIdForSubscribers(e.currentTarget.value)}></input>
          <input type="number" value={subscribersPage} onChange={(e) => setSubscribersPage(e.currentTarget.value)}></input>
          <input type="number" value={subscribersCount} onChange={(e) => setSubscribersCount(e.currentTarget.value)}></input>
          <button onClick={() => getSubscribersByUserId(userIdForSubscribers, subscribersPage, subscribersCount)}>
            Get user subscribers
          </button>
        </p>

        <p>
          <input type="number" value={userIdForPfpictures} onChange={(e) => setUserIdForPfpictures(e.currentTarget.value)}></input>
          <input type="number" value={pfpPage} onChange={(e) => setPfpPage(e.currentTarget.value)}></input>
          <input type="number" value={pfpCount} onChange={(e) => setPfpCount(e.currentTarget.value)}></input>
          <button onClick={() => getPfpicsByUserId(userIdForPfpictures, pfpPage, pfpCount)}>
            Get user pfp links
          </button>
        </p>

        <p>
          <input type="number" value={userIdForChats} onChange={(e) => setUserIdForChats(e.currentTarget.value)}></input>
          <input type="number" value={userChatsPage} onChange={(e) => setUserChatsPage(e.currentTarget.value)}></input>
          <input type="number" value={userChatsCount} onChange={(e) => setUserChatsCount(e.currentTarget.value)}></input>
          <button onClick={() => getChatsByUserId(userIdForChats, userChatsPage, userChatsCount)}>
            Get user chats
          </button>
        </p>

        <p>
          <input type="number" value={chatIdForUsers} onChange={(e) => setChatIdForUsers(e.currentTarget.value)}></input>
          <input type="number" value={chatsPage} onChange={(e) => setChatsPage(e.currentTarget.value)}></input>
          <input type="number" value={chatsCount} onChange={(e) => setChatsCount(e.currentTarget.value)}></input>
          <button onClick={() => getUsersByChatId(chatIdForUsers, chatsPage, chatsCount)}>
            Get chat users
          </button>
        </p>

        <p>
          <input type="number" value={chatIdForMessages} onChange={(e) => setChatIdForMessages(e.currentTarget.value)}></input>
          <input type="number" value={messagePage} onChange={(e) => setMessagePage(e.currentTarget.value)}></input>
          <input type="number" value={messageCount} onChange={(e) => setMessageCount(e.currentTarget.value)}></input>
          <button onClick={() => getMessagesByChatId(chatIdForMessages, messagePage, messageCount)}>
            Get chat messages
          </button>
        </p>
        <p>
          <input type="number" value={chatIdForPinMessages} onChange={(e) => setChatIdForPinMessages(e.currentTarget.value)}></input>
          <input type="number" value={pinPage} onChange={(e) => setPinPage(e.currentTarget.value)}></input>
          <input type="number" value={pinCount} onChange={(e) => setPinCount(e.currentTarget.value)}></input>
          <button onClick={() => getPinnedMessagesByChatId(chatIdForPinMessages, pinPage, pinCount)}>
            Get chat pinned messages
          </button>
        </p>
      </div>
    </>
  )
}

export default Test
