import React, { FunctionComponent } from "react";
import { Card, CardBody, Row, Col } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import ChallengeLogo from "components/Challenge/Logo";
import ChallengeSummary from "components/Challenge/Summary";
import { getChallengeDetailsPath } from "utils/coreui/constants";
import { Challenge } from "types/challenges";

type Props = { challenge: Challenge };

const Item: FunctionComponent<Props> = ({ challenge }) => (
  <Card>
    <LinkContainer
      to={getChallengeDetailsPath(challenge.id)}
      style={{
        cursor: "pointer"
      }}
    >
      <CardBody>
        <Row>
          <Col md="3">
            <ChallengeLogo
              className="h-100 bg-gray-900"
              height={150}
              width={150}
            />
          </Col>
          <Col md="9">
            <ChallengeSummary challengeId={challenge.id} />
          </Col>
        </Row>
      </CardBody>
    </LinkContainer>
  </Card>
);

export default Item;
