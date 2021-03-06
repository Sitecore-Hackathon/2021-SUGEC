import React, { useContext, useRef, useState, useEffect } from 'react';

import Annotation from '../components/Annotation';
import AnnotationModal from '../components/AnnotationModal';

import { Context } from '../context';

const AnnotationsLayer = () => {
  const {
    comments,
    openComment,
    setOpenComment,
    userName,
    connected,
    loading
  } = useContext(Context);

  const [location, setLocation] = useState({ left: 0, top: 0 });
  const [isOpen, setIsOpen] = useState(false);
  const containerRef = useRef(null);

  useEffect(() => {
    if (openComment !== null) {
      setIsOpen(false);
    }
  }, [openComment]);

  const handleClick = e => {
    if (!userName || !connected || loading) return;
    const bounds = containerRef.current.getBoundingClientRect();
    const left = e.clientX - bounds.left;
    const top = e.clientY - bounds.top;

    setOpenComment(null);
    setLocation({ left, top });
    setIsOpen(true);
  };

  return (
    <div className="annotations-layer" ref={containerRef} onClick={handleClick}>
      {comments.map((comment, index) =>
        comment.Location ? (
          <Annotation
            key={index}
            {...comment}
            isOpen={openComment === index}
            onClick={() => setOpenComment(openComment === index ? null : index)}
          />
        ) : null
      )}
      <AnnotationModal
        Location={location}
        isOpen={isOpen}
        onClose={() => setIsOpen(false)}
      />
    </div>
  );
};

export default AnnotationsLayer;
