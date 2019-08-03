import React from 'react';
import { Card, Accordion } from 'react-bootstrap';

import Scrollbar from 'react-scrollbars-custom';

import Participants from './Participants';

const ChallengeScoreboard = ({ challenge }) => (
  <>
    <Card bg="dark" className="my-2 text-light text-center">
      <Card.Header as="h5" className="border-0">
        Scoreboard
      </Card.Header>
    </Card>
    <Scrollbar
      style={{
        height: '500px'
      }}
    >
      <Accordion>
        <Participants challenge={challenge} />
      </Accordion>
    </Scrollbar>
  </>
);

export default ChallengeScoreboard;
