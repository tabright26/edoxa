import React, { Component } from "react";
import { Row, Col, Card, CardHeader } from "reactstrap";
import { withArenaChallengesContainer } from "../../../Container";

import ChallengeItem from "./Item";

class ArenaChallengeIndex extends Component {
  render() {
    return (
      <>
        <Row>
          <Col>
            <Card className="mt-4">
              <CardHeader tag="h5" className="text-center">
                League of Legends
              </CardHeader>
            </Card>
          </Col>
        </Row>
        <Row>
          {this.props.challenges
            .sort((left, right) => (left.timestamp < right.timestamp ? -1 : 1))
            .map((challenge, index) => (
              <Col key={index} xs="6" sm="4" md="3">
                <ChallengeItem challenge={challenge} />
              </Col>
            ))}
        </Row>
      </>
    );
  }
}

export default withArenaChallengesContainer(ArenaChallengeIndex);
