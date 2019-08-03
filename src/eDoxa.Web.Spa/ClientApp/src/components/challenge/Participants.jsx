import React from 'react';
import { Card } from 'react-bootstrap';

import Spinner from '../UI/Spinner';

import Participant from './Participant';

const Participants = ({ challenge }) => {
  if (!challenge) {
    return (
      <Card.Body className="text-center mt-5">
        <Spinner />
      </Card.Body>
    );
  } else {
    return (
      <>
        {challenge.participants
          .sort((left, right) =>
            left.averageScore < right.averageScore ? 1 : -1
          )
          .map((participant, index) => (
            <Participant
              key={index}
              participant={participant}
              position={index + 1}
            />
          ))}
      </>
    );
  }
};

export default Participants;
