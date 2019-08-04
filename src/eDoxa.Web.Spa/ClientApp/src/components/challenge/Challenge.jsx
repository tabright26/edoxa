import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Container, Row, Col } from 'react-bootstrap';

import ChallengeLogo from './ChallengeLogo';
import ChallengeSetup from './ChallengeSetup';
import ChallengeScoreboard from './ChallengeScoreboard';
import ChallengeScoring from './ChallengeScoring';
import ChallengePayout from './ChallengePayout';

import faker from 'faker';

import { findChallenge } from '../../store/actions/arenaChallengeActions';

faker.seed(1);

class Challenge extends Component {
  componentDidMount() {
    // Check if the challenge exists in the store.
    if (!this.props.challenge) {
      // If the challenge does not exist, dispatch a redux action to find the challenge with the API call.
      // This call will update the store with the HTTP response call.
      this.props.actions.findChallenge();
    }
  }

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

const mapStateToProps = (state, ownProps) => {
  return {
    challenge: state.arena.challenges.find(
      challenge => challenge.id === ownProps.match.params.challengeId
    )
  };
};

const mapDispatchToProps = (dispatch, ownProps) => {
  return {
    actions: {
      findChallenge: () =>
        dispatch(findChallenge(ownProps.match.params.challengeId))
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Challenge);
