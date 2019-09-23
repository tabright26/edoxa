import React, { Component } from 'react';
import { Container, Row, Col } from "reactstrap";

import ChallengeLogo from './Logo';
import ChallengeSetup from './Specification';
import ChallengeScoreboard from './Scoreboard';
import ChallengeScoring from './Scoring';
import ChallengePayout from './Payout';

import withArenaChallengeContainer from 'store/arena/challenges/connectArenaChallenge';

class Challenge extends Component {
  render() {
    return (
      <Container fluid={true}>
        <Row>
          <ChallengeLogo />
          <Col xs={4} className="ml-4">
            <ChallengeSetup challenge={this.props.challenge} />
          </Col>
        </Row>
        <Row>
          <Col xs={10}>
            <ChallengeScoreboard challenge={this.props.challenge} />
          </Col>
          <Col xs={2}>
            <Row>
              <Col xs={12}>
                <ChallengeScoring challenge={this.props.challenge} />
              </Col>
              <Col xs={12}>
                <ChallengePayout challenge={this.props.challenge} />
              </Col>
            </Row>
          </Col>
        </Row>
      </Container>
    );
  }
}

export default withArenaChallengeContainer(Challenge);
