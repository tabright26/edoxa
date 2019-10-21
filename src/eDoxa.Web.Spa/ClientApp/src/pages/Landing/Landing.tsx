import React from "react";
import { Container, Card, CardImg, CardImgOverlay, Button, Nav, NavbarBrand, Row, Col } from "reactstrap";
import { AppFooter } from "@coreui/react";
import { LinkContainer } from "react-router-bootstrap";
import panel from "assets/img/brand/panel.jpg";
import logo from "assets/img/brand/logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCoins, faTrophy, faArchway } from "@fortawesome/free-solid-svg-icons";

import clashroyalePanel from "assets/img/arena/games/clashroyale/panel.png";
import leagueoflegendsPanel from "assets/img/arena/games/leagueoflegends/panel.jpg";
import fortnitePanel from "assets/img/arena/games/fortnite/panel.png";
import "pure-react-carousel/dist/react-carousel.es.css";

import opticgamingClan from "assets/img/organization/clan/opticgaming.png";
import teamliquidClan from "assets/img/organization/clan/teamliquid.png";
import teamsolomidClan from "assets/img/organization/clan/teamsolomid.png";

const Landing = () => (
  <div className="app">
    <main className="main">
      <Card className="m-0 p-0 border-0">
        <CardImg height="500" src={panel} />
        <CardImgOverlay className="d-flex p-0">
          <Container className="text-center h-100">
            <header className="mt-2 mb-5 bg-transparent border-0 navbar nav">
              <LinkContainer to="/">
                <NavbarBrand>
                  <img src={logo} width={150} height={60} />
                </NavbarBrand>
              </LinkContainer>
              <Nav className="ml-auto mr-3">
                <Button color="link" style={{ textDecoration: "none" }} className="d-inline">
                  Login
                </Button>
                <Button href={`${process.env.REACT_APP_AUTHORITY}/Identity/Account/Register`} tag="a" className="d-inline ml-2" color="primary" outline>
                  Register
                </Button>
              </Nav>
            </header>
            <div className="my-auto">
              <h1 className="text-uppercase">PLAY FOR YOUR DREAM</h1>
              <p className="mb-5">
                Be the reason for your Clan's <span className="text-primary">success</span> by winning Challenges for <span className="text-primary">CASH</span>!
              </p>
              <span className="d-block">LIVE 2 PLAY</span>
              <Button size="lg" color="primary" className="my-3">
                PLAY 2 WIN
              </Button>
              <span className="d-block">WIN 4 GLORY</span>
            </div>
          </Container>
        </CardImgOverlay>
      </Card>
      <div className="bg-gray-900 py-5 border border-primary border-left-0 border-right-0 border-bottom-0">
        <Container>
          <h5 className="text-uppercase mb-5">How does it work?</h5>
          <div className="text-muted mt-5 d-flex justify-content-around">
            <div className="position-relative">
              <FontAwesomeIcon icon={faTrophy} size="8x" />
              <span
                className="text-primary text-center position-absolute"
                style={{ lineHeight: "0.9", fontSize: "17px", fontWeight: "bold", left: "50%", top: "50%", transform: "translate(-50%, -50%)", width: "125px" }}
              >
                <strong>JOIN</strong>
                <br />
                <small>A CHALLENGE</small>
              </span>
            </div>
            <div className="position-relative">
              <FontAwesomeIcon icon={faArchway} size="8x" />
              <span
                className="text-primary text-center position-absolute"
                style={{ lineHeight: "0.9", fontSize: "17px", fontWeight: "bold", left: "50%", top: "50%", transform: "translate(-50%, -50%)", width: "125px" }}
              >
                <strong>PLAY</strong>
                <br />
                <small>THE GAME</small>
              </span>
            </div>
            <div className="position-relative">
              <FontAwesomeIcon icon={faCoins} size="8x" />
              <span
                className="text-primary text-center position-absolute"
                style={{ lineHeight: "0.9", fontSize: "17px", fontWeight: "bold", left: "50%", top: "50%", transform: "translate(-50%, -50%)", width: "125px" }}
              >
                <strong>COLLECT</strong>
                <br />
                <small>WIN AND AUTOMATICALLY GET YOUR MONEY</small>
              </span>
            </div>
          </div>
        </Container>
      </div>
      <div className="bg-gray-700 py-5 border border-primary border-left-0 border-right-0 border-bottom-0">
        <Container>
          <h5 className="text-uppercase">Join a clan</h5>
          <Row>
            <Col md={4} className="mt-4">
              <ul className="mb-0">
                <li className="mb-4">Find the perfect clan</li>
                <li className="my-4">Create bonds with your teammates</li>
                <li className="my-4">Hone your skills</li>
                <li className="mt-4">Propel your eSport career</li>
              </ul>
            </Col>
            <Col md={8} className="d-flex justify-content-center align-items-center">
              <div>
                <img src={opticgamingClan} className="rounded" style={{ width: "175px", height: "175px" }} />
                <Button className="mt-2" color="primary" block>
                  JOIN NOW
                </Button>
              </div>
              <div className="mx-3">
                <img src={teamliquidClan} className="rounded" style={{ width: "175px", height: "175px" }} />
                <Button className="mt-2" color="primary" block>
                  JOIN NOW
                </Button>
              </div>
              <div>
                <img src={teamsolomidClan} className="rounded" style={{ width: "175px", height: "175px" }} />
                <Button className="mt-2" color="primary" block>
                  JOIN NOW
                </Button>
              </div>
            </Col>
          </Row>
          {/* <div className="mt-4 d-flex justify-content-between"></div> */}
        </Container>
      </div>
      <div className="bg-gray-900 py-5 border border-primary border-left-0 border-right-0 border-bottom-0">
        <Container>
          <h5 className="text-uppercase">Games</h5>
          <div className="d-flex w-auto mt-4">
            <img src={leagueoflegendsPanel} className="mr-2 w-100" style={{ minWidth: "325px", minHeight: "250px", borderRadius: "20px" }} />
            <img src={fortnitePanel} className="mx-2 w-100" style={{ minWidth: "325px", minHeight: "250px", borderRadius: "20px" }} />
            <img src={clashroyalePanel} className="ml-2 w-100" style={{ minWidth: "325px", minHeight: "250px", borderRadius: "20px" }} />
          </div>
        </Container>
      </div>
      <div className="bg-gray-700 py-5 border border-primary border-left-0 border-right-0">
        <Container>
          <h5 className="text-uppercase">Tournaments and Challenges</h5>
        </Container>
      </div>
    </main>
    <AppFooter className="bg-gray-900">
      <div className="mx-auto">&copy; {new Date(Date.now()).getFullYear()} eDoxa - All rights reserved.</div>
    </AppFooter>
  </div>
);

export default Landing;
