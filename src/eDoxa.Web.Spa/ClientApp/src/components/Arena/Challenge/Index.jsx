import React, { Component } from 'react';
import {
  Table,
  Container,
  Row,
  Col,
  Card,
  Jumbotron,
  Badge
} from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Moment from 'react-moment';

import Scrollbar from 'react-scrollbars-custom';

import { withArenaChallengesContainer } from './Container';

class ArenaChallengeIndex extends Component {
  render() {
    return (
      <>
        <Jumbotron className="mb-0 bg-dark rounded-0">
          <Container>
            <h1 className="text-center text-white mb-0">Challenges</h1>
          </Container>
        </Jumbotron>
        <Container fluid>
          <Row>
            <Col>
              <Card bg="dark" className="mt-4 text-light text-center rounded-0">
                <Card.Header as="h5" className="border-0 rounded-0">
                  League of legends
                </Card.Header>
              </Card>
            </Col>
          </Row>
          <Row>
            <Col xs={12}>
              <Scrollbar
                style={{
                  height: '800px'
                }}
                className="mt-4"
              >
                <Table
                  variant="dark"
                  responsive
                  className="mb-0 text-center"
                  id="challenges"
                >
                  <thead>
                    <tr>
                      <th className="border-top-0">Name</th>
                      <th className="border-top-0">Created at</th>
                      <th className="border-top-0">State</th>
                      <th className="border-top-0">Entries</th>
                      <th className="border-top-0">Payout entries</th>
                      <th className="border-top-0">Entry fee</th>
                      <th className="border-top-0">Best of</th>
                      <th className="border-top-0">Prize pool</th>
                    </tr>
                  </thead>
                  <tbody>
                    {this.props.challenges
                      .sort((left, right) =>
                        left.timestamp < right.timestamp ? -1 : 1
                      )
                      .map((challenge, index) => (
                        <LinkContainer
                          key={index}
                          to={'/challenges/' + challenge.id}
                        >
                          <tr>
                            <td>{challenge.name}</td>
                            <td>
                              <Moment unix format="ll">
                                {challenge.timeline.createdAt}
                              </Moment>
                            </td>
                            <td>
                              <Badge variant="primary">{challenge.state}</Badge>
                            </td>
                            <td className="align-middle">
                              {/* <ProgressBar
                                variant="primary"
                                now={challenge.participants.length}
                                max={challenge.setup.entries}
                                label={`${challenge.participants.length}/${challenge.setup.entries}`}
                              /> */}
                            </td>
                            <td>{/*challenge.setup.payoutEntries*/}</td>
                            <td>
                              {/* <CurrencyFormat
                                currency={challenge.setup.entryFee.currency}
                                amount={challenge.setup.entryFee.amount}
                              /> */}
                            </td>
                            <td>{challenge.bestOf}</td>
                            <td>
                              {/* <CurrencyFormat
                                currency={challenge.payout.prizePool.currency}
                                amount={challenge.payout.prizePool.amount}
                              /> */}
                            </td>
                          </tr>
                        </LinkContainer>
                      ))}
                  </tbody>
                </Table>
              </Scrollbar>
            </Col>
          </Row>
        </Container>
      </>
    );
  }
}

export default withArenaChallengesContainer(ArenaChallengeIndex);
