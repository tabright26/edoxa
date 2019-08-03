import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Container, Row, Col, Card, Button, Image } from 'react-bootstrap';

import { fetchLeagueOfLegendAccountByName } from '../../store/actions/gameAccountActions';

class UserOverview extends Component {
  constructor(props) {
    super(props);

    if (this.props.user != null && this.props.user.leagueId != null) {
      this.state = {
        verified: true
      };
    } else {
      this.state = {
        verified: false,
        leagueName: ''
      };
    }

    this.handleLeagueNameSearch = this.handleLeagueNameSearch.bind(this);
  }

  handleLeagueNameSearch(event) {
    this.setState({ leagueName: event.target.value });
  }

  render() {
    var leagueInfo = '';
    if (this.props.league) {
      leagueInfo =
        (<br />,
        <label for="icon">Current icon active on account</label>,
        <br />,
        <Image src="holder.js/171x180" roundedCircle />,
        <br />,
        <label for="region"></label>,
        <br />,
        <label for="leagueId"></label>);
    }

    if (this.state.verified) {
      return (
        <Container>
          <Row>
            <Col>
              <Card style={{ width: '18rem' }}>
                <Card.Img variant="top" src="https://via.placeholder.com/150" />
                <Card.Body>
                  <Card.Title>You are already verified</Card.Title>
                  <Card.Text>this.state.leagueId</Card.Text>
                </Card.Body>
              </Card>
            </Col>
          </Row>
        </Container>
      );
    } else {
      return (
        <Container>
          <Row>
            <Col>
              <Card style={{ width: '18rem' }}>
                <Card.Img variant="top" src="https://via.placeholder.com/150" />
                <Card.Body>
                  <Card.Title>Link your League of Legends account</Card.Title>
                  <div>
                    <label for="icon">Summoner name</label>
                    <input
                      type="text"
                      value={this.state.leagueName}
                      onChange={this.handleLeagueNameSearch}
                    />
                  </div>
                </Card.Body>
                <Button
                  variant="primary"
                  type="button"
                  onClick={fetchLeagueOfLegendAccountByName(
                    this.state.leagueName
                  )}
                >
                  Search
                </Button>
                {leagueInfo}
              </Card>
            </Col>
          </Row>
        </Container>
      );
    }
  }
}

const mapStateToProps = state => {
  return { user: null };
};

const mapDispatchToProps = dispatch => {
  return {
    actions: {
      fetchLeagueOfLegendAccountByName: () =>
        dispatch(fetchLeagueOfLegendAccountByName())
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(UserOverview);
