import React, { useContext, useRef, useState } from 'react';

import Annotation from '../components/Annotation';
import AnnotationModal from '../components/AnnotationModal';

import { Context } from '../context';

const AnnotationsLayer = () => {
  const { comments, userName, connected, loading } = useContext(Context);
  const [location, setLocation] = useState({ left: 0, top: 0 });
  const [isOpen, setIsOpen] = useState(false);
  const containerRef = useRef(null);

  const handleClick = e => {
    if (!userName || !connected || loading) return;
    const bounds = containerRef.current.getBoundingClientRect();
    const left = e.clientX - bounds.left;
    const top = e.clientY - bounds.top;

    setLocation({ left, top });
    setIsOpen(true);
  };

  return (
    <div className="annotations-layer" ref={containerRef} onClick={handleClick}>
      {comments
        .filter(comment => !!comment.Location)
        .map((comment, index) => (
          <Annotation key={index} {...comment} isOpen={true} />
        ))}
      <AnnotationModal
        Location={location}
        isOpen={isOpen}
        onClose={() => setIsOpen(false)}
      />
    </div>
  );
};

export default AnnotationsLayer;
