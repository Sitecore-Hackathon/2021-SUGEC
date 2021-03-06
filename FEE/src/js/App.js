import React, { useContext, useState, useRef, useEffect } from 'react';
import PropTypes from 'prop-types';
import classnames from 'classnames';

import { Context } from './context';

import AnnotationsLayer from './modules/AnnotationsLayer';
import CommentsPanel from './modules/CommentsPanel';

const App = ({ src }) => {
  const { annotationsActive } = useContext(Context);

  const iframe = useRef(null);
  const [loaded, setLoaded] = useState(false);
  const [height, setHeight] = useState(null);

  useEffect(() => {
    const { current } = iframe;
    if (loaded) {
      setHeight(current.contentDocument.body.offsetHeight);
    }
  }, [loaded, iframe.current?.contentDocument.body?.offsetHeight]);

  return (
    <div className="external-reviewers">
      <div
        className={classnames('preview', {
          'preview--annotations': annotationsActive
        })}
        style={{ height }}
      >
        {annotationsActive && <AnnotationsLayer />}
        <iframe
          src={src}
          frameBorder="0"
          ref={iframe}
          onLoad={() => setLoaded(true)}
        />
      </div>

      <CommentsPanel />
    </div>
  );
};

App.propTypes = {
  src: PropTypes.string.isRequired
};

export default App;
