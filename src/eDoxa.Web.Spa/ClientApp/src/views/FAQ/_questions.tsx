import React, { ReactNode } from "react";
import {
  getAccountRegisterPath,
  getProfileGamesPath,
  getChallengesPath,
  getPasswordForgotPath
} from "utils/coreui/constants";
import { Link } from "react-router-dom";
import { Badge } from "reactstrap";

interface Question {
  title: string;
  content: ReactNode[];
}

const questions: Question[] = [
  {
    title: "Why EDOXA WAS CREATED?",
    content: [
      <p className="mb-0">
        We were frustrated as gamers to have a limited number of options to be
        successful. eDoxa was born from the sear to help unlock every gamer’s
        potential so that they can be proud to live from their eSport dream.
        Thus, everything we do and every new feature we develop is required to
        push our users in their path to success. We believe so strongly in our
        mission that we will give throughout the year 5%-10% of our revenue back
        in form of tokens to all participants that won’t make money in our
        Challenges. This will give them the opportunity to always try and to
        never lose hope in making it in this cutthroat world.{" "}
      </p>
    ]
  },
  {
    title: "WHAT TO EXPECT IN THE FUTURE FOR EDOXA?",
    content: [
      <p className="mb-0">
        We are only at the beginning of our journey to become the #1 enabler of
        eSport talent in the world. We have a robust road map for the product,
        and this will change based on the feedback we get from you. Please
        contact us here or you can contact us{" "}
        <Link to={process.env.REACT_APP_DISCORD_URL}>here</Link> at{" "}
        <Link to="emailto:support@edoxa.gg">support@edoxa.gg</Link>.
      </p>
    ]
  },
  {
    title: "WHAT IS EDOXA?",
    content: [
      <p className="mb-0">
        eDoxa is a web platform that allows casual gamers to develop their
        skills and generate money so that they achieve their eSport goals and
        realize their dreams by participating in Challenges.
      </p>
    ]
  },
  {
    title: "HOW DO I SIGN UP?",
    content: [
      <p className="mb-0">
        You can sign up on eDoxa by clicking{" "}
        <Link to={getAccountRegisterPath()}>here</Link>.
      </p>
    ]
  },
  {
    title: "HOW DO I PARTICIPATE TO CHALLENGES ON EDOXA WITHIN 5 MINUTES?",
    content: [
      <ol className="mb-0">
        <li>
          <Link to={getAccountRegisterPath()}>Create an Account</Link> on eDoxa.
        </li>
        <li>
          <Link to={getProfileGamesPath()}>Synchronize and validate</Link> your
          League of Legends summoner name.
        </li>
        <li>
          <Link to={getChallengesPath()}>
            Select and register to a Challenge
          </Link>{" "}
          of your choice.
        </li>
        <li>
          When the Challenge is in the state{" "}
          <Badge color="primary">In-Progress</Badge>, load up the game client
          and play solo/duo ranked matches inside the client.
        </li>
        <li>
          Get automatically rewarded according to your ranking in the Challenge.
        </li>
      </ol>
    ]
  },
  {
    title: "HOW DO I RESET MY PASSWORD?",
    content: [
      <p className="mb-0">
        Click <Link to={getPasswordForgotPath()}>here</Link> to reset your
        password.
      </p>
    ]
  },
  {
    title:
      "WHAT HAPPENS IF MY OPPONENT OR I GET DISCONNECTED DURING AN ONLINE MATCH?",
    content: [
      <p className="mb-0">
        It is the responsibility of each player to ensure that their connection
        is suitable for playing online games. On the other hand, a disconnection
        will not have a big impact on your position since you can play as many
        games as you want in the respected timeline of the Challenge.
      </p>
    ]
  },
  {
    title: "WHAT IS A CHALLENGE?",
    content: [
      <p className="mb-0">
        We organize on eDoxa.gg multiple Challenges with different criteria for
        all types of games. Users can participate to any free, paid or sponsored
        Challenges on our website at any time from anywhere. The Challenges
        consist of accumulating the most points by following our scorecard in a
        specific time frame. Challenges allows players to play at their own
        leisure since they aren’t playing against the other participants inside
        the same Challenge. For example, you must play 3 solo ranked matches on
        League of Legends to be eligible in the prize pool in the next 24/48/72
        hours.
      </p>
    ]
  },
  {
    title: "CAN WE REGISTER TO MULTIPLE CHALLENGES?",
    content: [<p className="mb-0">Yes, you can.</p>]
  },
  {
    title:
      "WHAT GAME MODE NEEDS TO BE PLAYED FOR LEAGUE OF LEGENDS CHALLENGES?",
    content: [<p className="mb-0">Solo/Duo Ranked</p>]
  },
  {
    title: "WHAT’S A TIMELINE STATE?",
    content: [
      <p>
        A Challenge timeline can vary between 24 hours to 72 hours. The timer
        starts whenever the room is full.
      </p>,
      <p>There are 4 steps in our timeline:</p>,
      <ol className="mb-0">
        <li>
          <h6>Inscription</h6>
          <ul>
            <li>Challenge entries aren’t full yet.</li>
            <li>Challenge cannot start.</li>
            <li>
              Games that have been played before the Challenge starts won’t be
              taken in consideration for the final score.
            </li>
          </ul>
        </li>
        <li>
          <h6>Started/In-Progress</h6>
          <ul>
            <li>Challenge room is full of participants.</li>
            <li>Timer has started (you get notified by email).</li>
            <li>You can now start playing your solo/duo ranked games.</li>
          </ul>
        </li>
        <li>
          <h6>Ended</h6>
          <ul>
            <li>Timer has ended.</li>
            <li>
              Games that have been started after the end time won’t be taken in
              consideration for the final score.
            </li>
          </ul>
        </li>
        <li>
          <h6>Closed</h6>
          <ul>
            <li>Scores are completely updated.</li>
            <li>Prizes gets distributed accordingly.</li>
          </ul>
        </li>
      </ol>
    ]
  },
  {
    title: "WHAT IS YOUR SCORING LEGEND?",
    content: [<></>]
  },
  {
    title: "HOW DOES THE SCORECARD WORKS?",
    content: [<></>]
  },
  {
    title: "WHAT TYPE OF CHALLENGES DO YOU OFFER?",
    content: [<></>]
  },
  {
    title: "IS THERE A MINIMUM NUMBER OF GAMES I MUST PLAY TO GET PAID?",
    content: [<></>]
  },
  {
    title: "WHAT ARE THE ENTRY FEES FOR A CHALLENGE?",
    content: [<></>]
  },
  {
    title: "HOW MANY GAMES DOES EDOXA SUPPORTS?",
    content: [<></>]
  },
  {
    title: "HOW LONG DOES IT TAKE TO SEE MY SCORE IN A CHALLENGE?",
    content: [<></>]
  },
  {
    title: "WHAT IS THE PAYOUT STRUCTURE?",
    content: [<></>]
  },
  {
    title: "WHO IS ELIGIBLE TO PLAY ON EDOXA?",
    content: [<></>]
  },
  {
    title: "HOW TO ADD A CREDIT CARD?",
    content: [<></>]
  },
  {
    title: "HOW DO I DEPOSIT MONEY IN MY WALLET?",
    content: [<></>]
  },
  {
    title: "HOW DO I ADD MY BANK ACCOUNT?",
    content: [<></>]
  },
  {
    title: "HOW DO I WITHDRAW MONEY?",
    content: [<></>]
  },
  {
    title: "WHAT ARE THE REQUIREMENTS TO WITHDRAW MONEY?",
    content: [<></>]
  },
  {
    title: "WHAT ARE THE REQUIREMENTS TO DEPOSIT MONEY?",
    content: [<></>]
  },
  {
    title: "WHO IS YOUR PLAYMENT PROVIDER?",
    content: [<></>]
  },
  {
    title: "IS EDOXA LEGAL?",
    content: [<></>]
  },
  {
    title: "WHAT IS SKILLED BASED GAMING?",
    content: [<></>]
  },
  {
    title: "DOES MY STATE OR PROVINCE ALLOW SKILL-BASED GAMING?",
    content: [<></>]
  },
  {
    title: "WHAT IS THE LEGAL AGE TO PLAY ON eDoxa.gg?",
    content: [<></>]
  },
  {
    title: "ARE YOU A PARTNER OR RIOT?",
    content: [<></>]
  },
  {
    title: "WHAT IS A SMURF?",
    content: [<></>]
  },
  {
    title: "WHAT ARE THE CONSEQUENCES OF HAVING A SMURF ACCOUNT?",
    content: [<></>]
  },
  {
    title: "WHAT IS YOUR REFUND POLICY?",
    content: [<></>]
  },
  {
    title: "IS eDoxa.gg SECURE?",
    content: [<></>]
  }
];

export default questions;
