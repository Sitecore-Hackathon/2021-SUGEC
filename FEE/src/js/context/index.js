import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';

import useApi from '../hooks/useApi';

const Context = React.createContext();

const Provider = ({ children }) => {
  const [userName, setUserName] = useState(
    sessionStorage.getItem('commentsUsername', null)
  );
  const [annotationsActive, setAnnotationsActive] = useState(false);
  const [returnCheck, setReturnCheck] = useState(false);

  const { comments, postComment, connected, loading } = useApi(
    '/api/sitecore/comments'
  );

  useEffect(() => {
    userName && sessionStorage.setItem('commentsUsername', userName);
  }, [userName]);

  return (
    <Context.Provider
      value={{
        userName,
        setUserName,
        annotationsActive,
        setAnnotationsActive,
        returnCheck,
        setReturnCheck,
        comments,
        postComment,
        connected,
        loading
      }}
    >
      {children}
    </Context.Provider>
  );
};

Provider.propTypes = {
  children: PropTypes.node.isRequired
};

export { Context, Provider };
