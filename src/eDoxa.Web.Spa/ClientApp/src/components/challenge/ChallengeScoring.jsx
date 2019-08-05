import React from 'react';
import { Card, ListGroup, ListGroupItem } from 'react-bootstrap';

import Spinner from '../Shared/Spinner';

const Body = ({ challenge }) => {
  if (!challenge) {
    return (
      <Card.Body className="text-center">
        <Spinner />
      </Card.Body>
    );
  } else {
    return (
      <ListGroup>
        {Object.entries(challenge.scoring).map((item, index) => (
          <ListGroupItem key={index} className="px-2 py-1 bg-dark">
            {item[0]}
            <span className="float-right">{item[1]}</span>
          </ListGroupItem>
        ))}
      </ListGroup>
    );
  }
};

const ChallengeScoring = ({ challenge }) => {
  return (
    <Card bg="dark" className="my-2 text-light">
      <Card.Header as="h5" className="text-center">
        Scoring
      </Card.Header>
      <Body challenge={challenge} />
    </Card>
  );
};

export default ChallengeScoring;