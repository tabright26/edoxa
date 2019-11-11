import React from "react";
import { CardTitle, Row, Col, Badge, Progress } from "reactstrap";

import Format from "components/Shared/Format";

const ArenaChallengeSpecification = ({ name, game, bestOf, entries, entryFee, payoutEntries, participantCount }) => (
  <Row>
    <Col>
      <CardTitle className="mt-2 pb-2 border-bottom border-primary">
        <strong className="text-uppercase">Summary</strong>
      </CardTitle>
      <Row>
        <Col>
          <dl className="row mb-0">
            <dd className="col-5 text-muted">Name</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {name}
              </Badge>
            </dt>
            <dd className="col-5 text-muted">Game</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {game}
              </Badge>
            </dt>
            <dd className="col-5 text-muted">Entry fee</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                <Format.Currency alignment="center" currency={entryFee.currency} amount={entryFee.amount} />
              </Badge>
            </dt>
          </dl>
        </Col>
        <Col>
          <dl className="row mb-0">
            <dd className="col-5 text-muted">Entries</dd>
            <dt className="col-7 text-right">
              <Progress color="dark" value={participantCount} max={entries} style={{ borderRadius: "10rem" }}>
                {`${participantCount}/${entries}`}
              </Progress>
            </dt>
            <dd className="col-5 text-muted">Payout entries</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {payoutEntries}
              </Badge>
            </dt>
            <dd className="col-5 text-muted">Best of</dd>
            <dt className="col-7 text-right">
              <Badge color="dark" pill className="w-100">
                {bestOf}
              </Badge>
            </dt>
          </dl>
        </Col>
      </Row>
    </Col>
  </Row>
);

export default ArenaChallengeSpecification;
