import { useState } from 'react'
import '../styles/App.css'
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
} from '../services/requests'

function App() {
  const [userIdForPosts, setUserIdForPosts] = useState(1);
  const [postIdForReactions, setPostIdForReactions] = useState(2);
  const [postIdForLikes, setPostIdForLikes] = useState(3);
  const [userIdForSubscribers, setUserIdForSubscribers] = useState(1);
  const [userIdForPfpictures, setUserIdForPfpictures] = useState(2);
  const [userIdForChats, setUserIdForChats] = useState(2);
  const [chatIdForUsers, setChatIdForUsers] = useState(1);
  const [chatIdForMessages, setChatIdForMessages] = useState(1);
  const [chatIdForPinMessages, setChatIdForPinMessages] = useState(2);

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
          Get user posts by User ID
          <input type="number" value={userIdForPosts} onChange={(e) => setUserIdForPosts(e.currentTarget.value)}></input>
          <button onClick={() => getPostsByUserId(userIdForPosts)}>
            Execute
          </button>
        </p>
        <p>
          Get reactions by Post ID
          <input type="number" value={postIdForReactions} onChange={(e) => setPostIdForReactions(e.currentTarget.value)}></input>
          <button onClick={() => getReactionsByPostId(postIdForReactions)}>
            Execute
          </button>
        </p>
        <p>
          Get likes by Post ID
          <input type="number" value={postIdForLikes} onChange={(e) => setPostIdForLikes(e.currentTarget.value)}></input>
          <button onClick={() => getLikesByPostId(postIdForLikes)}>
            Execute
          </button>
        </p>

        <p>
          Get Emoji list
          <button onClick={() => getEmojis()}>
            Execute
          </button>
        </p>

        <p>
          Get user subscribers
          <input type="number" value={userIdForSubscribers} onChange={(e) => setUserIdForSubscribers(e.currentTarget.value)}></input>
          <button onClick={() => getSubscribersByUserId(userIdForSubscribers)}>
            Execute
          </button>
        </p>
        <p>
          Get user pfp links
          <input type="number" value={userIdForPfpictures} onChange={(e) => setUserIdForPfpictures(e.currentTarget.value)}></input>
          <button onClick={() => getPfpicsByUserId(userIdForPfpictures)}>
            Execute
          </button>
        </p>

        <p>
          Get user chats
          <input type="number" value={userIdForChats} onChange={(e) => setUserIdForChats(e.currentTarget.value)}></input>
          <button onClick={() => getChatsByUserId(userIdForChats)}>
            Execute
          </button>
        </p>
        <p>
          Get chat users
          <input type="number" value={chatIdForUsers} onChange={(e) => setChatIdForUsers(e.currentTarget.value)}></input>
          <button onClick={() => getUsersByChatId(chatIdForUsers)}>
            Execute
          </button>
        </p>
        <p>
          Get chat messages
          <input type="number" value={chatIdForMessages} onChange={(e) => setChatIdForMessages(e.currentTarget.value)}></input>
          <button onClick={() => getMessagesByChatId(chatIdForMessages)}>
            Execute
          </button>
        </p>
        <p>
          Get chat pinned messages
          <input type="number" value={chatIdForPinMessages} onChange={(e) => setChatIdForPinMessages(e.currentTarget.value)}></input>
          <button onClick={() => getPinnedMessagesByChatId(chatIdForPinMessages)}>
            Execute
          </button>
        </p>
      </div>
    </>
  )
}

export default App
