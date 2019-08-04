import React from 'react';
import { Card, Row, Col, Badge, ProgressBar } from 'react-bootstrap';

import CurrencyFormat from '../Shared/Formaters/CurrencyFormat';
import Spinner from '../Shared/Spinner';

import ChallengeTimeline from './ChallengeTimeline';

import faker from 'faker';

faker.seed(1);

const Body = ({ challenge }) => {
  if (!challenge) {
    return (
      <Card.Body className="text-center text-white">
        <Spinner />
      </Card.Body>
    );
  } else {
    return (
      <Card.Body className="d-flex">
        <ChallengeTimeline challenge={challenge} />
        <Row>
          <Col>
            <Card.Title as="h3">Challenge setup</Card.Title>
            <hr className="border-light" />
            <dl className="row mb-0 float-right">
              <dt className="col-5">Name</dt>
              <dd className="col-7">{challenge.name}</dd>
              <dt className="col-5">Game</dt>
              <dd className="col-7">
                <Badge variant="primary">{challenge.game}</Badge>
              </dd>
              <dt className="col-5">
                {/* Entry fee ({challenge.setup.entryFee.currency}) */}
              </dt>
              <dd className="col-7">
                {/* <CurrencyFormat
                  currency={challenge.setup.entryFee.currency}
                  amount={challenge.setup.entryFee.amount}
                /> */}
              </dd>
              <dt className="col-5">Entries</dt>
              <dd className="col-7 my-auto">
                <ProgressBar
                  variant="primary"
                  now={challenge.participants.length}
                  max={challenge.entries}
                  label={`${challenge.participants.length}/${challenge.entries}`}
                />
              </dd>
              <dt className="col-5 mt-2 mb-1">Payout entries</dt>
              <dd className="col-7 mt-2 mb-1">
                {/* {challenge.setup.payoutEntries} */}
              </dd>
              <dt className="col-5 mt-1">Best of</dt>
              <dd className="col-7 mt-1 mb-0">{challenge.bestOf}</dd>
            </dl>
          </Col>
        </Row>
      </Card.Body>
    );
  }
};

const ChallengeSetup = ({ challenge }) => (
  <Card bg="dark" className="my-4 text-light">
    <Body challenge={challenge} />
  </Card>
);

export default ChallengeSetup;
