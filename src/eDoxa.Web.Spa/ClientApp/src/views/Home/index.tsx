import React, { Suspense, useState, FunctionComponent } from "react";
import {
  Container,
  Card,
  Button,
  Nav,
  NavbarBrand,
  Row,
  Col,
  CardBody,
  CardHeader,
  CardImg,
  CardImgOverlay
} from "reactstrap";
import { AppFooter } from "@coreui/react";
import logo from "assets/img/brand/logo.png";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCoins,
  faTrophy,
  faArchway
} from "@fortawesome/free-solid-svg-icons";

//import clashroyalePanel from "assets/img/arena/games/clashroyale/panel.png";
//import clashroyaleLarge from "assets/img/arena/games/clashroyale/large.png";
import leagueoflegendsLarge from "assets/img/arena/games/leagueoflegends/large.png";
import leagueoflegendsPanel from "assets/img/arena/games/leagueoflegends/panel.jpg";
//import fortnitePanel from "assets/img/arena/games/fortnite/panel.png";
//import fortniteLarge from "assets/img/arena/games/fortnite/large.png";

import csgoLogoLg from "assets/img/arena/games/csgo/logo/lg.png";
import csgoPanel from "assets/img/arena/games/csgo/panel.jpg";
import dota2LogoLg from "assets/img/arena/games/dota2/logo/lg.png";
import dota2Panel from "assets/img/arena/games/dota2/panel.png";

//import clan1 from "assets/img/organization/clan/clan1.jpg";
//import clan2 from "assets/img/organization/clan/clan2.jpg";
//import clan3 from "assets/img/organization/clan/clan3.jpg";
import Layout from "components/App/Layout";
import { Loading } from "components/Shared/Loading";
import { LinkContainer } from "react-router-bootstrap";
import {
  getAccountRegisterPath,
  getChallengesPath,
  getProfileTransactionHistoryPath,
  getDefaultPath
} from "utils/coreui/constants";
import { ApplicationPaths } from "utils/oidc/ApiAuthorizationConstants";
import { IconDefinition } from "@fortawesome/fontawesome-svg-core";
import YouTube from "react-youtube";
import Beta from "components/App/Beta";

const Footer = React.lazy(() => import("components/App/Footer"));

interface GamePanelProps {
  logo: string;
  panel: string;
  maxHeight: string;
  commingSoon?: boolean;
  className?: string;
  to?: string;
}

const GamePanel: FunctionComponent<GamePanelProps> = ({
  logo,
  panel,
  maxHeight,
  commingSoon = false,
  className,
  to = null
}) => {
  const [hover, setHover] = useState(false);
  return (
    <Card
      className={`p-0 ${className}`}
      onMouseEnter={() => setHover(true)}
      onMouseLeave={() => setHover(false)}
      style={
        hover
          ? {
              cursor: commingSoon ? "mouse" : "pointer",
              borderRadius: "20px",
              width: "33.33%"
            }
          : { borderRadius: "20px", width: "33.33%" }
      }
    >
      <CardImg
        src={panel}
        style={{
          height: "200px",
          borderRadius: "20px",
          filter: commingSoon
            ? "grayscale(100%)"
            : hover
            ? "brightness(50%)"
            : null
        }}
      />
      {to ? (
        <LinkContainer to={to}>
          <CardImgOverlay className="d-flex">
            {hover ? (
              commingSoon ? (
                <h5 className="m-auto text-uppercase">Coming Soon...</h5>
              ) : (
                <h5 className="m-auto text-uppercase">Go to challenges...</h5>
              )
            ) : (
              <img
                src={logo}
                className="m-auto"
                alt="leagueoflegends"
                style={{
                  fontWeight: "bold",
                  maxHeight,
                  filter: commingSoon ? "grayscale(100%)" : null
                }}
              />
            )}
          </CardImgOverlay>
        </LinkContainer>
      ) : (
        <CardImgOverlay className="d-flex">
          {hover ? (
            commingSoon ? (
              <h5 className="m-auto text-uppercase">Coming Soon...</h5>
            ) : (
              <h5 className="m-auto text-uppercase">Go to challenges...</h5>
            )
          ) : (
            <img
              src={logo}
              className="m-auto"
              alt="leagueoflegends"
              style={{
                fontWeight: "bold",
                maxHeight,
                filter: commingSoon ? "grayscale(100%)" : null
              }}
            />
          )}
        </CardImgOverlay>
      )}
    </Card>
  );
};

interface GameIconProps {
  icon: IconDefinition;
  title: string;
  subtitle: string;
}

const GameIcon: FunctionComponent<GameIconProps> = ({
  icon,
  title,
  subtitle
}) => {
  const [hover, setHover] = useState(false);
  return (
    <div
      className="position-relative"
      onMouseOver={() => setHover(true)}
      onMouseLeave={() => setHover(false)}
    >
      <FontAwesomeIcon
        className={`${hover ? "text-primary" : "text-dark"}`}
        icon={icon}
        size="8x"
        style={{ cursor: hover ? "pointer" : "none" }}
      />
      <span
        className={`${
          hover ? "text-light" : "text-primary"
        } text-center position-absolute`}
        style={{
          lineHeight: "0.9",
          fontSize: "17px",
          fontWeight: "bold",
          left: "50%",
          top: "50%",
          transform: "translate(-50%, -50%)",
          width: "125px",
          cursor: hover ? "pointer" : "none"
        }}
      >
        <strong>{title}</strong>
        <br />
        <small>{subtitle}</small>
      </span>
    </div>
  );
};

const opts = {
  height: "200",
  width: "350"
};

const App = () => (
  <>
    <Beta.Banner />
    <Layout.Background>
      <Container className="text-center my-5 h-100">
        <header className="mb-5 bg-transparent border-0 navbar nav">
          <NavbarBrand style={{ cursor: "pointer" }}>
            <LinkContainer to={getDefaultPath()}>
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
              Build your new lifestyle by winning Challenges for{" "}
              <span className="text-primary">CASH</span>!
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
        <h3 className="text-uppercase">How does it work?</h3>
        <div className="d-flex">
          <YouTube opts={opts} className="mr-auto" videoId="tGvo3PmSRUc" />
          <div className="text-muted d-flex ml-auto w-100">
            <LinkContainer to={getChallengesPath()}>
              <div className="m-auto">
                <GameIcon icon={faTrophy} title="JOIN" subtitle="A CHALLENGE" />
              </div>
            </LinkContainer>
            <LinkContainer to={getChallengesPath()}>
              <div className="m-auto">
                <GameIcon icon={faArchway} title="PLAY" subtitle="THE GAME" />
              </div>
            </LinkContainer>
            <LinkContainer to={getProfileTransactionHistoryPath()}>
              <div className="m-auto">
                <GameIcon
                  icon={faCoins}
                  title="COLLECT"
                  subtitle="WIN AND AUTOMATICALLY GET YOUR MONEY"
                />
              </div>
            </LinkContainer>
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
                  Join a room of players competing for cash money or tokens.
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
                  Only your best matches played during the period of the
                  Challenge are considered in your final score.
                </span>
              </li>
              <li className="mt-4">
                <span className="text-white">
                  Your matches are not played against the other participants
                  allowing you to play your ranked games against players of your
                  own skill level and at your leisure.
                </span>
              </li>
            </ul>
          </Col>
          <Col md={{ size: 4, offset: 2 }}>
            <Card className="card-accent-primary">
              <CardHeader>
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
            <LinkContainer to={getChallengesPath()}>
              <Button size="lg" color="primary" className="mt-4">
                PLAY NOW
              </Button>
            </LinkContainer>
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
        <div className="d-flex mt-4 mx-auto">
          <GamePanel
            maxHeight="65px"
            className="mr-4"
            logo={csgoLogoLg}
            panel={csgoPanel}
            commingSoon
          />
          <GamePanel
            to={getChallengesPath()}
            maxHeight="85px"
            className="mx-4"
            logo={leagueoflegendsLarge}
            panel={leagueoflegendsPanel}
          />
          <GamePanel
            maxHeight="55px"
            className="ml-4"
            logo={dota2LogoLg}
            panel={dota2Panel}
            commingSoon
          />
        </div>
      </Container>
    </div>
    {/* <div
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
    </div> */}
    <AppFooter className="py-3">
      <Suspense fallback={<Loading />}>
        <Footer className="mx-auto" />
      </Suspense>
    </AppFooter>
  </>
);

export default App;
