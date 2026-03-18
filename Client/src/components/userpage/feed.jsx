import React from "react";
import { useEffect, useState } from "react";
import { getUserFeed } from '../../services/requests';

function Feed() {
    const [publications, setPublications] = useState([]);

    useEffect(() => {
    const loadFeed = async () => {
      const feedData = await getUserFeed();
      setPublications(feedData);
    };
    loadFeed();
  }, []);

  return (
    <div>
      <span className="fixed top-5 left-5 flex items-center justify-center gap-[0.5vw] p-[0.5vw] rounded-md transition-all">
        My Feed
      </span>
      {publications.map(pub => (
        <div key={pub.id} className="mt-4">
            <span className="mt-1 block font-bold">Author: {pub.publisher.displayedName}</span>
            <span className="mt-1 block underline">Published: {new Date(pub.date).toLocaleString('ru-RU')}</span>
            <span className="mt-1 block font">{pub.content}</span>
        </div>
      ))}
    </div>
  )
}

export default Feed;