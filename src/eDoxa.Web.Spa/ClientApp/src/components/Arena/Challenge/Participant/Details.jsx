import React from 'react';
import { Card, Badge, Accordion } from 'react-bootstrap';

import ScoreFormat from '../../../Shared/Formaters/ScoreFormat';
import Spinner from '../../../Shared/Spinner';

import Matches from './Match/Index';

const ArenaChallengeParticipantDetails = ({ participant, position }) => {
  if (!participant) {
    return (
      <Card.Body className="text-center mt-5">
        <Spinner />
      </Card.Body>
    );
  } else {
    return (
      <>
        <Accordion.Toggle
          as="div"
          eventKey={position - 1}
          className="participant"
        >
          <Card bg="dark" className="my-2 text-light">
            <Card.Body className="p-0 d-flex">
              <div
                className="pl-2 py-2 text-center"
                style={{
                  width: '45px'
                }}
              >
                <Badge variant="primary">{position}</Badge>
              </div>
              <div className="px-3 py-2">{participant.id}</div>
              <div
                className="bg-primary px-3 py-2 text-center ml-auto"
                style={{
                  width: '90px'
                }}
              >
                <ScoreFormat score={participant.averageScore} />
              </div>
            </Card.Body>
          </Card>
        </Accordion.Toggle>
        <Accordion.Collapse eventKey={position - 1}>
          <Card bg="dark" className="text-light">
            <Card.Header as="h5">Matches</Card.Header>
            <Matches participant={participant} />
          </Card>
        </Accordion.Collapse>
      </>
    );
  }
};

export default ArenaChallengeParticipantDetails;
