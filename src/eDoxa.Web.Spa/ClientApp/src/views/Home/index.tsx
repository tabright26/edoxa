import React, { Suspense } from "react";
import {
  Container,
  Card,
  Button,
  Nav,
  NavbarBrand,
  Row,
  Col,
  CardBody,
  CardHeader
} from "reactstrap";
import { AppFooter } from "@coreui/react";
import logo from "assets/img/brand/logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCoins,
  faTrophy,
  faArchway
} from "@fortawesome/free-solid-svg-icons";

import clashroyalePanel from "assets/img/arena/games/clashroyale/panel.png";
import clashroyaleLarge from "assets/img/arena/games/clashroyale/large.png";
import leagueoflegendsLarge from "assets/img/arena/games/leagueoflegends/large.png";
import leagueoflegendsPanel from "assets/img/arena/games/leagueoflegends/panel.jpg";
import fortnitePanel from "assets/img/arena/games/fortnite/panel.png";
import fortniteLarge from "assets/img/arena/games/fortnite/large.png";

import clan1 from "assets/img/organization/clan/clan1.jpg";
import clan2 from "assets/img/organization/clan/clan2.jpg";
import clan3 from "assets/img/organization/clan/clan3.jpg";
import Layout from "components/Shared/Layout";
import { Loading } from "components/Shared/Loading";
import { LinkContainer } from "react-router-bootstrap";
import {
  getAccountRegisterPath,
  getHomePath,
  getChallengesPath
} from "utils/coreui/constants";
import { ApplicationPaths } from "utils/oidc/ApiAuthorizationConstants";

const Footer = React.lazy(() => import("components/App/Footer"));

const App = () => (
  <>
    <Layout.Background>
      <Container className="text-center my-5 h-100">
        <header className="mb-5 bg-transparent border-0 navbar nav">
          <NavbarBrand>
            <LinkContainer to={getHomePath()}>
              <img src={logo} width={150} height={60} alt="brand" />
            </LinkContainer>
          </NavbarBrand>
          <Nav className="ml-auto mr-3">
            <LinkContainer to={ApplicationPaths.Login}>
              <Button
                size="lg"
                color="link"
                style={{ textDecoration: "none" }}
                className="d-inline"
              >
                Login
              </Button>
            </LinkContainer>
            <LinkContainer to={getAccountRegisterPath()}>
              <Button
                size="lg"
                href={`${process.env.REACT_APP_AUTHORITY}/Identity/Account/Register`}
                tag="a"
                className="d-inline ml-2"
                color="primary"
                outline
              >
                Register
              </Button>
            </LinkContainer>
          </Nav>
        </header>
        <div
          className="d-flex justify-content-center h-100"
          style={{
            fontSize: "20px"
          }}
        >
          <div
            className="align-items-center position-absolute my-auto w-100"
            style={{
              left: "50%",
              top: "50%",
              transform: "translate(-50%, -50%)",
              width: "125px"
            }}
          >
            <h1 className="text-uppercase">PLAY FOR YOUR DREAM</h1>
            <p className="mb-5">
              Be the reason for your Clan's{" "}
              <span className="text-primary">success</span> by winning
              Challenges for <span className="text-primary">CASH</span>!
            </p>
            <span className="d-block">LIVE 2 PLAY</span>
            <LinkContainer to={getChallengesPath()}>
              <Button size="lg" color="primary" className="my-3">
                PLAY 2 WIN
              </Button>
            </LinkContainer>
            <span className="d-block">WIN 4 GLORY</span>
          </div>
        </div>
      </Container>
    </Layout.Background>
    <div
      className="bg-gray-900 py-5 d-flex align-items-center"
      style={{ height: "350px" }}
    >
      <Container>
        <h3 className="text-uppercase mb-5">How does it work?</h3>
        <div className="text-muted mt-5 d-flex justify-content-around">
          <div className="position-relative">
            <FontAwesomeIcon className="text-dark" icon={faTrophy} size="8x" />
            <span
              className="text-primary text-center position-absolute"
              style={{
                lineHeight: "0.9",
                fontSize: "17px",
                fontWeight: "bold",
                left: "50%",
                top: "50%",
                transform: "translate(-50%, -50%)",
                width: "125px"
              }}
            >
              <strong>JOIN</strong>
              <br />
              <small>A CHALLENGE</small>
            </span>
          </div>
          <div className="position-relative">
            <FontAwesomeIcon className="text-dark" icon={faArchway} size="8x" />
            <span
              className="text-primary text-center position-absolute"
              style={{
                lineHeight: "0.9",
                fontSize: "17px",
                fontWeight: "bold",
                left: "50%",
                top: "50%",
                transform: "translate(-50%, -50%)",
                width: "125px"
              }}
            >
              <strong>PLAY</strong>
              <br />
              <small>THE GAME</small>
            </span>
          </div>
          <div className="position-relative">
            <FontAwesomeIcon className="text-dark" icon={faCoins} size="8x" />
            <span
              className="text-primary text-center position-absolute"
              style={{
                lineHeight: "0.9",
                fontSize: "17px",
                fontWeight: "bold",
                left: "50%",
                top: "50%",
                transform: "translate(-50%, -50%)",
                width: "125px"
              }}
            >
              <strong>COLLECT</strong>
              <br />
              <small>WIN AND AUTOMATICALLY GET YOUR MONEY</small>
            </span>
          </div>
        </div>
      </Container>
    </div>
    <div
      className="bg-gray-800 py-5 d-flex align-items-center"
      style={{ height: "550px" }}
    >
      <Container>
        <h3 className="text-uppercase">Join a clan</h3>
        <Row>
          <Col md={4} className="mt-4">
            <ul className="mb-0 text-primary text-justify">
              <li className="mb-4">
                <span className="text-white">Find the perfect clan</span>
              </li>
              <li className="my-4">
                <span className="text-white">
                  Create bonds with your teammates
                </span>
              </li>
              <li className="my-4">
                <span className="text-white">Hone your skills</span>
              </li>
              <li className="mt-4">
                <span className="text-white">Propel your eSport career</span>
              </li>
            </ul>
          </Col>
          <Col
            md={8}
            className="d-flex justify-content-center align-items-center"
          >
            <div>
              <img
                src={clan1}
                className="rounded"
                alt=""
                style={{ width: "175px", height: "175px" }}
              />
              <Button className="mt-2" color="primary" block>
                JOIN NOW
              </Button>
            </div>
            <div className="mx-3">
              <img
                src={clan2}
                className="rounded"
                alt=""
                style={{ width: "175px", height: "175px" }}
              />
              <Button className="mt-2" color="primary" block>
                JOIN NOW
              </Button>
            </div>
            <div>
              <img
                src={clan3}
                className="rounded"
                alt=""
                style={{ width: "175px", height: "175px" }}
              />
              <Button className="mt-2" color="primary" block>
                JOIN NOW
              </Button>
            </div>
          </Col>
        </Row>
      </Container>
    </div>
    <div
      className="bg-gray-900 py-5 d-flex align-items-center"
      style={{ height: "350px" }}
    >
      <Container>
        <h3 className="text-uppercase mb-4">Games</h3>
        <div className="d-flex w-auto mt-4">
          <div className="position-relative mr-4">
            <img
              src={leagueoflegendsPanel}
              className="w-100"
              alt=""
              style={{
                minWidth: "325px",
                height: "200px",
                borderRadius: "20px"
              }}
            />
            <img
              src={leagueoflegendsLarge}
              className="position-absolute"
              alt=""
              style={{
                fontWeight: "bold",
                left: "50%",
                top: "50%",
                transform: "translate(-50%, -50%)"
              }}
            />
          </div>
          <div className="position-relative mx-5">
            <img
              src={fortnitePanel}
              className="w-100"
              alt=""
              style={{
                minWidth: "325px",
                height: "200px",
                borderRadius: "20px"
              }}
            />
            <img
              src={fortniteLarge}
              className="position-absolute"
              alt=""
              style={{
                fontWeight: "bold",
                left: "50%",
                top: "50%",
                transform: "translate(-50%, -50%)"
              }}
            />
          </div>
          <div className="position-relative ml-4">
            <img
              src={clashroyalePanel}
              className="w-100"
              alt=""
              style={{
                minWidth: "325px",
                height: "200px",
                borderRadius: "20px"
              }}
            />
            <img
              src={clashroyaleLarge}
              className="position-absolute"
              alt=""
              style={{
                fontWeight: "bold",
                left: "50%",
                top: "50%",
                transform: "translate(-50%, -50%)"
              }}
            />
          </div>
        </div>
      </Container>
    </div>
    <div
      className="bg-gray-800 py-5 d-flex align-items-center"
      style={{ height: "550px" }}
    >
      <Container>
        <h3 className="text-uppercase">Challenges</h3>
        <Row className="d-flex justify-content-center align-items-center">
          <Col md={6} className="mt-4">
            <h5 className="mb-4">What is a challenge?</h5>
            <ul className="mb-0 text-primary text-justify">
              <li className="mb-4">
                <span className="text-white">
                  Join a room of players competing for cash or tokens.
                </span>
              </li>
              <li className="my-4">
                <span className="text-white">
                  Your Challenge points are calculated automatically based on
                  the stats played in-game.
                </span>
              </li>
              <li className="my-4">
                <span className="text-white">
                  Only the 3 best matches played during the period of the
                  Challenge are considered in your final score.
                </span>
              </li>
              <li className="mt-4">
                <span className="text-white">
                  Your matches are not played against the other participants
                  allowing you to play your ranked games against your own skill
                  level and at your leisure.
                </span>
              </li>
            </ul>
          </Col>
          <Col md={{ size: 4, offset: 2 }}>
            <Card className="card-accent-primary">
              <CardHeader className="bg-gray-900">
                <strong>SCORE WEIGHTING</strong>
              </CardHeader>
              <CardBody>
                <dl className="row mb-0">
                  <dt className="col-6 text-left">Kills</dt>
                  <dd className="col-6 text-right text-success">+4</dd>
                  <dt className="col-6 text-left">Assists</dt>
                  <dd className="col-6 text-right text-success">+3.5</dd>
                  <dt className="col-6 text-left">Deaths</dt>
                  <dd className="col-6 text-right text-danger">-4</dd>
                  <dt className="col-6 text-left">Minions</dt>
                  <dd className="col-6 text-right text-success">+0.04</dd>
                  <dt className="col-6 text-left">TotalDamageDealt</dt>
                  <dd className="col-6 text-right text-success">+0.0009</dd>
                  <dt className="col-6 text-left">TotalDamageTaken</dt>
                  <dd className="col-6 text-right text-success">+0.00125</dd>
                  <dt className="col-6 text-left">VisionScore</dt>
                  <dd className="col-6 text-right text-success">+0.38</dd>
                  <dt className="col-6 text-left mb-0">Win</dt>
                  <dd className="col-6 text-right text-success mb-0">+20</dd>
                </dl>
              </CardBody>
            </Card>
          </Col>
          <Col md={12} className="text-center">
            <Button size="lg" color="primary" className="mt-4">
              PLAY NOW
            </Button>
          </Col>
        </Row>
      </Container>
    </div>
    <AppFooter className="py-3">
      <Suspense fallback={<Loading />}>
        <Footer className="mx-auto" />
      </Suspense>
    </AppFooter>
  </>
);

export default App;
