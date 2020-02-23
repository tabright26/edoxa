import React, { ReactNode } from "react";
import {
  getAccountRegisterPath,
  getProfileGamesPath,
  getChallengesPath,
  getPasswordForgotPath,
  getProfileOverviewPath
} from "utils/coreui/constants";
import { Link } from "react-router-dom";
import { Badge } from "reactstrap";
import Support from "components/Shared/Support";
import { Discord } from "components/Shared/Social";

export const NEW_USERS_GROUP_ID = "NEW_USERS_GROUP";
export const ACCOUNT_GROUP_ID = "ACCOUNT_GROUP";
export const CHALLENGES_GROUP_ID = "CHALLENGES_GROUP";
export const CASHIER_GROUP_ID = "CASHIER_GROUP";
export const LEGALITY_GROUP_ID = "LEGALITY_GROUP";
export const RULES_GROUP_ID = "RULES_GROUP";
export const SECURITY_GROUP_ID = "SECURITY_GROUP";

export type QuestionGroupId =
  | typeof NEW_USERS_GROUP_ID
  | typeof ACCOUNT_GROUP_ID
  | typeof CHALLENGES_GROUP_ID
  | typeof CASHIER_GROUP_ID
  | typeof LEGALITY_GROUP_ID
  | typeof RULES_GROUP_ID
  | typeof SECURITY_GROUP_ID;

export interface QuestionGroup {
  id: QuestionGroupId;
  name: string;
  path: string;
}

export const questionGroups: QuestionGroup[] = [
  { id: NEW_USERS_GROUP_ID, name: "New users", path: "new-users" },
  { id: ACCOUNT_GROUP_ID, name: "Account", path: "account" },
  { id: CHALLENGES_GROUP_ID, name: "Challenges", path: "challenges" },
  { id: CASHIER_GROUP_ID, name: "Cashier", path: "cashier" },
  { id: LEGALITY_GROUP_ID, name: "Legality", path: "legality" },
  { id: RULES_GROUP_ID, name: "Rules", path: "rules" },
  { id: SECURITY_GROUP_ID, name: "Security", path: "security" }
];

export interface Question {
  groupId: QuestionGroupId;
  title: string;
  content: ReactNode[];
}

export const questions: Question[] = [
  {
    groupId: NEW_USERS_GROUP_ID,
    title: "WHY EDOXA WAS CREATED?",
    content: [
      <p>
        We were frustrated as gamers to have a limited number of options
        available to achieve online gaming success. eDoxa was born from the
        desire to help unlock every gamer’s potential, so that they could be
        proud to live their eSport dream.
      </p>,
      <p className="mb-0">
        Everything we do and every new feature we develop therefore is designed
        to push our users towards a path of success. We believe strongly in our
        mission and throughout the year will give <strong>5%-10%</strong> of our
        revenue, back in the form of tokens to all participants that don’t make
        money in our Challenges. This will give them the encouragement and
        opportunity to always try and to never lose hope in making it in this
        online world.
      </p>
    ]
  },
  {
    groupId: NEW_USERS_GROUP_ID,
    title: "WHAT TO EXPECT IN THE FUTURE FOR EDOXA?",
    content: [
      <p className="mb-0">
        We are only at the beginning of our journey to become the #1 enabler of
        eSport talent in the world. We have a robust road map for the product,
        and this will change based on the feedback we get from you. Please
        contact us <Discord>here</Discord> or you can contact us at{" "}
        <Support.EmailAddress />.
      </p>
    ]
  },
  {
    groupId: NEW_USERS_GROUP_ID,
    title: "WHAT IS EDOXA?",
    content: [
      <p className="mb-0">
        eDoxa is a web platform that allows casual gamers to develop their
        skills and generate money in order to achieve their eSport goals, and
        realize their dreams by participating in Challenges.
      </p>
    ]
  },
  {
    groupId: NEW_USERS_GROUP_ID,
    title: "HOW DO I SIGN UP?",
    content: [
      <p className="mb-0">
        You can sign up on eDoxa by clicking{" "}
        <Link to={getAccountRegisterPath()}>here</Link>.
      </p>
    ]
  },
  {
    groupId: NEW_USERS_GROUP_ID,
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
    groupId: NEW_USERS_GROUP_ID,
    title: "WHAT’S A DOXATAG?",
    content: [
      <p className="mb-0">A DoxaTag is what identifies you on eDoxa.gg.</p>
    ]
  },
  {
    groupId: NEW_USERS_GROUP_ID,
    title: "HOW DO I CREATE MY DOXATAG?",
    content: [
      <p className="mb-0">
        Create your DoxaTag <Link to={getProfileOverviewPath()}>here</Link>.
      </p>
    ]
  },
  {
    groupId: ACCOUNT_GROUP_ID,
    title: "HOW DO I RESET MY PASSWORD?",
    content: [
      <p className="mb-0">
        Click <Link to={getPasswordForgotPath()}>here</Link> to reset your
        password.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
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
    groupId: CHALLENGES_GROUP_ID,
    title: "WHAT IS A CHALLENGE?",
    content: [
      <p>
        We organize multiple Challenges with different criteria for all types of
        games on eDoxa.gg. Users can participate to any free, paid or sponsored
        Challenges on our website at anytime from anywhere. The Challenges
        consist of accumulating the most points by following our scorecard in a
        specific time frame.
      </p>,
      <p className="mb-0">
        Challenges allow players to play at their own leisure, since they aren’t
        playing against the other participants inside the same Challenge. For
        example, you must play 3 solo ranked matches on League of Legends to be
        eligible in the prize pool in the next 24/48/72 hours.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "CAN WE REGISTER TO MULTIPLE CHALLENGES?",
    content: [<p className="mb-0">Yes, you can.</p>]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title:
      "WHAT GAME MODE NEEDS TO BE PLAYED FOR LEAGUE OF LEGENDS CHALLENGES?",
    content: [<p className="mb-0">Solo/Duo Ranked</p>]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
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
    groupId: CHALLENGES_GROUP_ID,
    title: "WHAT IS YOUR SCORING LEGEND?",
    content: [<></>]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "DO YOU SUGGEST PLAYING SUPPORT ROLE IN MY GAMES?",
    content: [
      <p className="mb-0">
        Our goal is to have a fair scoring legend for each role. Based on user
        feedback we will make multiple adjustments.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "HOW DOES THE SCORECARD WORKS?",
    content: [
      <p>
        In order to rank you accordingly, we need to consider your game score
        and the weighting of the Challenge scorecard.
      </p>,
      <ul className="mb-0">
        <li>The value column represents your in-game score.</li>
        <li>The weighting column represents our score multiplier.</li>
        <li>
          By multiplying the value and the weighting of the action, an eDoxa
          score is generated.
        </li>
        <li>
          We add the eDoxa score of each action to obtain the total score of
          every player.
        </li>
      </ul>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "WHAT TYPE OF CHALLENGES DO YOU OFFER?",
    content: [
      <ul className="mb-0">
        <li>
          Best of 1: Your BEST SCORE within the Challenge timeline will be taken
          into consideration to determine your position.
        </li>
        <li>
          Best of 3: AN AVERAGE of your top three scores within the Challenge
          timeline will be taken into consideration to determine your position.
        </li>
        <li>
          Best of 5: AN AVERAGE of your top five scores within the Challenge
          timeline will be taken into consideration to determine your position.
        </li>
        <li>
          Best of 7: AN AVERAGE of your top seven scores within the Challenge
          timeline will be taken into consideration to determine your position.
        </li>
      </ul>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "IS THERE A MINIMUM NUMBER OF GAMES I MUST PLAY TO GET PAID?",
    content: [
      <p className="mb-0">
        Yes, depending on the Challenge type (Best of 1/3/5/7) you must play a
        minimum of 1-7 games within the time period. If you don’t play the
        minimum required, you will automatically lose the Challenge.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "WHAT ARE THE ENTRY FEES FOR A CHALLENGE?",
    content: [
      <p className="mb-0">
        At eDoxa, we offer a wide variety of Challenges at different prices to
        meet your needs. You can enter a Challenge for free, 2$, 3$, 5$, 10$,
        20$, 50$, 100$ or with tokens.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "HOW MANY GAMES DOES EDOXA SUPPORT?",
    content: [
      <p className="mb-0">
        Every Challenge is made for a specific game. Currently, our Challenges
        support League of Legends. We are planning on adding multiple new games
        in the following months.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "HOW LONG DOES IT TAKE TO SEE MY SCORE IN A CHALLENGE?",
    content: [
      <p className="mb-0">
        Every ~30 minutes, the Challenge updates to display new scores.
      </p>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "WHAT IS THE PAYOUT STRUCTURE?",
    content: [
      <ul className="mb-0">
        <li>Top 50% will win a prize.</li>
        <li>
          The payout distribution is dependent on the number of participants and
          the entry fee.
        </li>
        <li>Bottom 50% will get a consolation prize of 250 tokens.</li>
      </ul>
    ]
  },
  {
    groupId: CHALLENGES_GROUP_ID,
    title: "WHO IS ELIGIBLE TO PLAY ON EDOXA?",
    content: [
      <p className="mb-0">
        Everybody of all skill levels, from in Canada or the United States of
        America that respects our eSports code of conduct and terms of
        conditions are welcomed on eDoxa!
      </p>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "HOW TO ADD A CREDIT CARD?",
    content: [
      <ol className="mb-0">
        <li>Go to Payment Methods.</li>
        <li>
          In the cards section, select the button{" "}
          <Badge className="text-uppercase">ADD A NEW CREDIT CARD</Badge>.
        </li>
        <li>
          Enter your credit card information and hit the <Badge>Save</Badge>{" "}
          button.
        </li>
      </ol>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "HOW CAN I BE ELLIGIBLE FOR A DEPOSIT?",
    content: [
      <ol className="mb-0">
        <li>
          Add your credit card information in the Payment Methods section.
        </li>
        <li>Verify your account with the email verification we sent you.</li>
      </ol>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "HOW DO I DEPOSIT MONEY IN MY WALLET?",
    content: [
      <ol className="mb-0">
        <li>
          Hover over the money section next to the menu (Upper right corner).
        </li>
        <li>Select the “Deposit” button.</li>
        <li>
          Select the amount of money that you want to deposit into your wallet.
        </li>
        <li>Select the “Confirm” button.</li>
      </ol>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "HOW DO I WITHDRAW MONEY?",
    content: [
      <ol className="mb-0">
        <li>
          Hover over the money section next to the menu (Upper right corner)
        </li>
        <li>Select the “Withdraw” button</li>
        <li>
          Select the amount of money that you want eDoxa to wire to your bank
          account.
        </li>
        <li>Select the “Confirm” button</li>
      </ol>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "WHAT ARE THE REQUIREMENTS TO DEPOSIT MONEY?",
    content: [
      <ul className="mb-0">
        <li>Your credit card must be added on eDoxa.</li>
        <li>Deposit a minimum of $10.00.</li>
      </ul>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "WHAT ARE THE REQUIREMENTS TO WITHDRAW MONEY?",
    content: [
      <ul className="mb-0">
        <li>You can use your PAYPAL account to withdraw.</li>
        <li>Withdraw is a minimum of $50.00 / transaction.</li>
      </ul>
    ]
  },
  {
    groupId: CASHIER_GROUP_ID,
    title: "WHO IS YOUR PAYMENT PROVIDER?",
    content: [
      <p className="mb-0">
        All financial data and transactions are handled by Stripe and PAYPAL.
      </p>
    ]
  },
  {
    groupId: LEGALITY_GROUP_ID,
    title: "IS EDOXA LEGAL?",
    content: [
      <p className="mb-0">
        Yes. Challenges on eDoxa.gg are legal under federal law and under most
        provincial/state laws. eDoxa.gg only hosts video games of skill on the
        website and not of luck, thus receiving the specific exemption from the
        2006 Unlawful Internet Gambling Enforcement Act (UIGEA).
      </p>
    ]
  },
  {
    groupId: LEGALITY_GROUP_ID,
    title: "WHAT IS SKILLED BASED GAMING?",
    content: [
      <p>
        eDoxa.gg is considered skilled gaming. We supply Challenges for users to
        compete globally for cash and prizes.
      </p>,
      <p>
        Skill-based gaming has a well-established legal, social and commercial
        history. From classic board games to major sports tournaments, games of
        skill have long offered participants a chance to compete based on one’s
        ability instead of chance.
      </p>,
      <p className="mb-0">
        <strong>Online skill-based games</strong> are online games in which the
        outcome of the game is determined by the player's physical skill (like
        fast reaction or dexterity) or mental skill (logic abilities, strategic
        thinking, trivia knowledge). As in off-line games of skill, the
        definition has a legal meaning, as playing games of chance for money is
        an illegal act in several countries.
      </p>
    ]
  },
  {
    groupId: LEGALITY_GROUP_ID,
    title: "DOES MY STATE OR PROVINCE ALLOW SKILL-BASED GAMING?",
    content: [
      <p className="mb-0">
        Most states and provinces consider playing video games for cash legal,
        as long as you are playing a game predominantly requiring skill to win.
        The only exception is Louisiana and Montana.
      </p>
    ]
  },
  {
    groupId: LEGALITY_GROUP_ID,
    title: "WHAT IS THE LEGAL AGE TO PLAY ON eDoxa.gg?",
    content: [
      <p className="mb-0">
        You must be at least 18 years old to open an account, participate in
        Challenges or win prizes.
      </p>
    ]
  },
  {
    groupId: LEGALITY_GROUP_ID,
    title: "ARE YOU A PARTNER OR RIOT?",
    content: [
      <p className="mb-0">
        eDoxa isn’t endorsed by Riot Games and doesn’t reflect the views or
        opinions of Riot Games or anyone officially involved in producing or
        managing League of Legends. League of Legends and Riot Games are
        trademarks or registered trademarks of Riot Games, Inc. League of
        Legends © Riot Games, Inc.
      </p>
    ]
  },
  {
    groupId: RULES_GROUP_ID,
    title: "WHAT IS A SMURF?",
    content: [
      <p className="mb-0">
        In multiplayer online gaming, the term "Smurf" (noun) is used to refer
        to an experienced player who creates a new or secondary account for the
        purposes of being matched against inexperienced players for easier
        opponents.
      </p>
    ]
  },
  {
    groupId: RULES_GROUP_ID,
    title: "WHAT ARE THE CONSEQUENCES OF HAVING A SMURF ACCOUNT?",
    content: [
      <p className="mb-0">
        After an internal investigation has been completed and the account is
        deemed a Smurf, it will be permanently suspended from eDoxa.gg. You can
        also report Smurf accounts at <Support.EmailAddress />.
      </p>
    ]
  },
  {
    groupId: RULES_GROUP_ID,
    title: "WHAT IS YOUR REFUND POLICY?",
    content: [
      <p className="mb-0">
        In most cases, eDoxa.gg does not allow refunds once a result has been
        finalized. If you believe a mistake is made on your part, please contact
        us at <Support.EmailAddress /> and we can sort out a possible solution.
      </p>
    ]
  },
  {
    groupId: SECURITY_GROUP_ID,
    title: "IS eDoxa.gg SECURE?",
    content: [
      <p className="mb-0">
        Security is a priority at eDoxa. We make sure to use the most
        sophisticated and secure technology to protect your sensitive
        information at all times.
      </p>
    ]
  }
];
