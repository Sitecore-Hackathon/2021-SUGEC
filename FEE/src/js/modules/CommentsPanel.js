import React, { useContext, useState, useEffect, useRef } from 'react';
import classnames from 'classnames';
import Switch from 'react-switch';

import ExpandButton from '../components/ExpandButton';
import CollapseButton from '../components/CollapseButton';
import Comment from '../components/Comment';
import LoginForm from '../components/LoginForm';
import CommentForm from '../components/CommentForm';

import { Context } from '../context';

const CommentsPanel = () => {
  const {
    userName,
    setUserName,
    annotationsActive,
    setAnnotationsActive,
    comments,
    postComment
  } = useContext(Context);

  const commentsRef = useRef(null);
  const [expanded, setExpanded] = useState(true);

  useEffect(() => {
    const { current } = commentsRef;
    current.scrollTop = current.scrollHeight;
  }, [comments, userName]);

  const handlePostComent = Body => postComment({ UserName: userName, Body });

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
        <div className="comments-panel__switch">
          <p>Annotations mode</p>
          <Switch
            onChange={() => setAnnotationsActive(!annotationsActive)}
            checked={annotationsActive}
            onColor="#1ca6a3"
            uncheckedIcon={false}
            checkedIcon={false}
          />
        </div>
        <div className="comments-panel__messages" ref={commentsRef}>
          {comments.map((comment, index) => (
            <Comment key={index} {...comment} />
          ))}
        </div>

        {!!userName && <CommentForm onSubmit={handlePostComent} />}
        {!userName && <LoginForm onSubmit={setUserName} />}
      </div>
    </div>
  );
};

export default CommentsPanel;
