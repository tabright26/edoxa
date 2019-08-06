import React, { Component } from 'react';
import { Card, Badge } from 'react-bootstrap';

import Spinner from '../../../../Shared/Spinner';
import ScoreFormat from '../../../../Shared/Formaters/ScoreFormat';
import Moment from 'react-moment';

import Stats from './Stat/Index';

class Match extends Component {
  constructor(props) {
    super(props);

    this.state = { modalShow: false };
  }

  render() {
    const { match, position } = this.props;
    let modalClose = () => this.setState({ modalShow: false });
    if (!match) {
      return (
        <Card.Body className="text-center">
          <Spinner />
        </Card.Body>
      );
    } else {
      return (
        <>
          <Card.Body className="p-0 border border-dark d-flex">
            <div
              className="pl-2 py-2 text-center"
              style={{
                width: '45px'
              }}
            >
              <Badge variant="light">{position}</Badge>
            </div>
            <div
              className="px-3 py-2"
              style={{
                width: '350px'
              }}
            >
              <Moment unix format="LLLL">
                {match.synchronizedAt}
              </Moment>
            </div>
            <div
              className="py-2 text-center mx-auto"
              onClick={() => this.setState({ modalShow: true })}
            >
              <Badge variant="primary">View details</Badge>
            </div>
            <div
              className="bg-primary px-3 py-2 text-center ml-5"
              style={{
                width: '90px'
              }}
            >
              <ScoreFormat score={match.totalScore} />
            </div>
          </Card.Body>
          <Stats
            show={this.state.modalShow}
            onHide={modalClose}
            stats={match.stats}
          />
        </>
      );
    }
  }
}

export default Match;
