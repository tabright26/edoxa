import React from "react";
import { Container, Card, CardImg, CardImgOverlay, Button } from "reactstrap";
import logo from "../../assets/images/Rectangle 2.jpg";

const Home = () => (
  <>
    <Card className="m-0 p-0 border-0">
      <CardImg height="400" src={logo} />
      <CardImgOverlay className="d-flex">
        <Container className="text-center my-auto">
          <h1 className="text-uppercase">PLAY FOR YOUR DREAM</h1>
          <p className="mb-5">
            Be the reason for your Clan's <span className="text-primary">success</span> by winning Challenges for <span className="text-primary">CASH</span>!
          </p>
          <span className="d-block">LIVE 2 PLAY</span>
          <Button size="lg" color="primary" className="my-3">
            PLAY 2 WIN
          </Button>
          <span className="d-block">WIN 4 GLORY</span>
        </Container>
      </CardImgOverlay>
    </Card>
    <div className="bg-gray-900 py-5 border border-primary border-left-0 border-right-0 border-bottom-0">
      <Container>
        <h5 className="text-uppercase">How does it work?</h5>
      </Container>
    </div>
    <div className="py-5 border border-primary border-left-0 border-right-0 border-bottom-0">
      <Container>
        <h5 className="text-uppercase">Join a clan</h5>
      </Container>
    </div>
    <div className="bg-gray-900 py-5 border border-primary border-left-0 border-right-0 border-bottom-0">
      <Container>
        <h5 className="text-uppercase">Games</h5>
      </Container>
    </div>
    <div className="py-5 border border-primary border-left-0 border-right-0">
      <Container>
        <h5 className="text-uppercase">Tournaments and Challenges</h5>
      </Container>
    </div>
  </>
);

export default Home;
