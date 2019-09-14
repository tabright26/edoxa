import React from "react";
import { Card, CardBody, CardHeader, Row, Col, Badge, Progress } from "reactstrap";

//import Format from "../../../../components/Format";
import Loading from "../../Shared/Loading";

import ChallengeTimeline from "./Timeline";

const Body = ({ challenge }) => {
  if (!challenge) {
    return (
      <CardBody className="text-center text-white">
        <Loading.Default />
      </CardBody>
    );
  } else {
    return (
      <CardBody className="d-flex">
        <ChallengeTimeline challenge={challenge} />
        <Row>
          <Col>
            <CardHeader as="h3">Challenge setup</CardHeader>
            <hr className="border-light" />
            <dl className="row mb-0 float-right">
              <dt className="col-5">Name</dt>
              <dd className="col-7">{challenge.name}</dd>
              <dt className="col-5">Game</dt>
              <dd className="col-7">
                <Badge variant="primary">{challenge.game}</Badge>
              </dd>
              <dt className="col-5">{/* Entry fee ({challenge.setup.entryFee.currency}) */}</dt>
              <dd className="col-7">
                {/* <CurrencyFormat
                  currency={challenge.setup.entryFee.currency}
                  amount={challenge.setup.entryFee.amount}
                /> */}
              </dd>
              <dt className="col-5">Entries</dt>
              <dd className="col-7 my-auto">
                <Progress color="primary" value={challenge.participants.length} max={challenge.entries} label={`${challenge.participants.length}/${challenge.entries}`} />
              </dd>
              <dt className="col-5 mt-2 mb-1">Payout entries</dt>
              <dd className="col-7 mt-2 mb-1">{/* {challenge.setup.payoutEntries} */}</dd>
              <dt className="col-5 mt-1">Best of</dt>
              <dd className="col-7 mt-1 mb-0">{challenge.bestOf}</dd>
            </dl>
          </Col>
        </Row>
      </CardBody>
    );
  }
};

const ArenaChallengeSpecification = ({ challenge }) => (
  <Card bg="dark" className="my-4 text-light">
    <Body challenge={challenge} />
  </Card>
);

export default ArenaChallengeSpecification;
