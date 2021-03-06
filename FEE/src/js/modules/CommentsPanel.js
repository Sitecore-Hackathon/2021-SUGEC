import React, { useState, useEffect, useRef } from 'react';
import classnames from 'classnames';

import ExpandButton from '../components/ExpandButton';
import CollapseButton from '../components/CollapseButton';
import Comment from '../components/Comment';
import LoginForm from '../components/LoginForm';
import CommentForm from '../components/CommentForm';

import useApi from '../hooks/useApi';

const CommentsPanel = () => {
  const commentsRef = useRef(null);
  const [expanded, setExpanded] = useState(false);
  const [userName, setUserName] = useState(
    sessionStorage.getItem('commentsUsername', null)
  );

  const { comments, postComment, connected, loading } = useApi('/api/comments');

  useEffect(() => {
    userName && sessionStorage.setItem('commentsUsername', userName);
  }, [userName]);

  useEffect(() => {
    const { current } = commentsRef;
    current.scrollTop = current.scrollHeight;
  }, [comments, userName]);

  const handlePostComent = Body => {
    postComment({ UserName: userName, Body });
  };

  return (
    <div
      className={classnames('comments-panel', {
        'comments-panel--expanded': expanded
      })}
    >
      <ExpandButton onClick={() => setExpanded(true)} hidden={expanded} />
      <div className="comments-panel__header">
        <h2>Comments</h2>
        <CollapseButton onClick={() => setExpanded(false)} />
      </div>
      <div className="comments-panel__inner">
        <div className="comments-panel__messages" ref={commentsRef}>
          {comments.map((comment, index) => (
            <Comment key={index} {...comment} />
          ))}
        </div>

        {!!userName && (
          <CommentForm
            onSubmit={handlePostComent}
            disabled={!connected || loading}
          />
        )}
        {!userName && <LoginForm onSubmit={setUserName} />}
      </div>
    </div>
  );
};

export default CommentsPanel;
