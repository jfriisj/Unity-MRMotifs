EPICSAVE - Enhancing Vocational Training for 
Paramedics with Multi-user Virtual Reality 
Jonas Schild 
Hochschule Hannover 
University of Applied 
Sciences and Arts 
Hannover, Germany 
Dieter Lerner 
 Fraunhofer Institute for 
Experimental Software 
Engineering IESE 
Kaiserslautern, Germany  
Sebastian Misztal  
Hochschule Hannover 
University of Applied 
Sciences and Arts 
Hannover, Germany 
Thomas Luiz 
 Fraunhofer Institute for 
Experimental Software 
Engineering IESE 
Kaiserslautern, Germany 
 
 
Abstract— An anaphylactic shock constitutes a representative 
scenario for critical paramedical cases that happen too rare to 
eventually occur within a regular curricular term of vocational 
training. As a possible solution, this paper presents EPICSAVE, a 
development case that yields novel training tools using multi-user 
virtual reality (VR) and serious game methodology. The case 
describes the interdisciplinary setup and iterative workflow of the 
development of a simulation prototype. Examples show design 
tools and methodologies, e.g., finding focus in medical treatment. 
Results from two pilot studies indicate that specifically multi-user 
VR may enhance paramedic training. The subsequently developed 
prototype offers collaborative training for two paramedic trainees 
and one trainer. Results from a user study with paramedic trainees 
indicate that experiencing a positive VR training outcome depends 
on high presence effects and is limited by usability issues. We 
provide a list of open design and usability issues that shall help to 
improve future integration of multi-user VR in training facilities. 
Keywords— medical vocational training; virtual reality; 
educational virtual environments; multi-user VR:, simulation; 
serious game; 
I. INTRODUCTION 
A. May VR Serious Games enhance paramedic training? 
Rare exposure to critical emergencies, such as the severe 
anaphylactic shock, makes practical training within the limited 
time frame of a vocational curriculum challenging [1]. Such 
emergencies typically involve varying team constellations and 
environments, limited patient information, low tolerance to 
errors, and time pressure. Besides structured procedures based 
on medical algorithms, experience helps paramedics to develop 
competence in coordination of task- and teamwork in 
dynamically 
changing 
situations. 
Such 
comprehensive 
competence can hardly be acquired through existing training 
methods, i.e. involving high-fidelity simulators based on 
simulation phantoms or even professional actors. Additionally, 
such methods require high effort of personnel, material and time, 
with limitations in offering a simulation of dynamically 
fluctuating symptoms and vital signs, involving several organ 
systems (e.g. skins, airways,  cardiovascular system) [2, 3]. As 
solution to this lack of training of collaborative tasks in dynamic 
settings, we propose developing novel training tools based on 
multi-user VR and serious game technology (see Fig. 1), as also 
regarded promising by other researchers [4]. 
 
Fig. 1. 
The EPICSAVE simulation prototype in use with two paramedic 
trainees in multi-user VR and two trainers. 
B. Challenges and opportunities 
Key challenges are the technical and content-wise 
development of such new training tools and their integration into 
vocational training centers. While recent evolution of consumer 
VR technology and game development tools can drastically 
increase resulting quality of interactive simulations, the possible 
scaling of investment is incredibly large, looking at successful 
commercial game productions now reaching more than 50 Mio $ 
budgets, with some games costing several hundreds of Millions. 
What kind of investment is necessary to create a significantly 
advantageous effect, which overall extends existing training 
methods and hence improves quality of training?  
C. Finding focus in interdisciplinary, iterative development 
Simulation fidelity and graphical quality directly scale with 
investment, 
requiring 
personal-intensive 
3D 
modeling, 
animation, texturing, and material shader editing. Hence, we 
must generally reduce complexity; we must find focus in 
medical and technical content, to provide interactivity, decision-
making, and collaboration on a relying level. Such a relying 
level is a compromise of realism vs. abstraction, aiming at 
supporting users in transferring competence towards real 
situations. Finding such compromises is one of many tasks in a 
development process that leads to a successful VR serious game. 
According to Dörner et al. [5], the general methodology for 
978-1-5386-6298-4/18/$31.00 ©2018 IEEE
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
creating such novel media typically involves experts from 
multiple areas developing iterative steps through agile software 
engineering. The actual process depends on the actual problem 
space and needs adaptation, called “design your design” [6]. The 
goal of this paper is to pave the way for other developers by 
presenting, describing and assessing a concrete development 
case: A interdisciplinary project consortium develops a 
practicing system for paramedical vocational training that aims 
at combining serious game technology with virtual reality in 
concert with curricular modeling and implementation at 
paramedic services. The development process takes three years 
with two iterations, one simulation prototype and one 
subsequent serious game prototype to follow. This paper 
describes the development case of the simulation prototype, i.e., 
the setup, requirements analysis, development, and evaluation, 
between March 2016 and November 2017. 
 
II. RELATED WORK 
Virtual Reality (VR) technologies and Serious Games (SG) 
have become powerful and promising tools in education because 
of their unique technological characteristics. A large number of 
studies shows that both technologies support effective teaching 
and learning. 
A. Virtual Reality-Simulation 
Immersive virtual environments (VE) have been adopted as 
cost-effective solutions for creating simulations in a vast set of 
areas, including healthcare and emergency medicine. Multi-user 
VEs enable social interaction through several multisensory 
channels; they also support coordination actions, allow 
cooperation settings, and improve collaborative learning [7, 8]. 
A meta-analysis by Consorti et al. of 12 studies showed a clear 
positive overall effect of using virtual patients as educational 
interventions, compared to other educational methods [9]. The 
authors define a virtual patient as an interactive computer 
simulation of real-life clinical scenarios for healthcare and 
medical training, education or assessment. Merchant et al. [10] 
examined in their meta-analysis the impact of selected 
instructional design principles in the context of desktop-based 
VR-based instruction for the categories of games (13 studies), 
simulations (29 studies) and virtual worlds (25 studies). The 
meta-analysis revealed statistically significant positive effects of 
all three categories on learning. A moderator analysis further 
showed higher learning gains for games than for simulations and 
virtual worlds.  
Flores et al. [11] describe a clinical case simulator called 
SimDeCS using a 2D non-multi-user environment with focus on 
model-learning supported by artificial intelligence. A student 
can enter a dialog with a virtual patient. Interactions between 
agents use textual dialogs. Results from applying a self-designed 
questionnaire indicate that participants find SimDeCS 
motivational; they believe that it can be an effective remedy 
education tool and the students retain knowledge after using it. 
Zielke et al. [12] evaluated advantages and disadvantages of VR 
and augmented reality in patient applications. The authors 
describe drawbacks of their learning platform "UT TIME 
Portal", e.g., text- and monitor-based interface, less immersive 
and less natural experiences. They see a considerable potential 
in using a 3D environment with head-mounted displays. 
Students further suggested to implement non-verbal techniques 
like body language and eye contact to switch attention between 
patient and caregiver. 
B. Serious Games 
Recent literature reviews and meta-analysis confirm the 
impact of games on learning [13]: A meta-analysis, carried out 
by Wouters et al. analyzed 39 studies focusing on comparisons 
of knowledge, skills, retention, and motivation outcomes in 
serious games. Wouters et al. found that SG were more effective 
than conventional instruction in terms of learning and retention, 
but they found no evidence that SG were more motivating [14]. 
In their systematic review of serious games in training health 
care 
professionals 
Wang 
et 
al. 
synthesized 
current 
serious gaming trends in health care training. 19 studies (for 33 
games) reported learning effects, whereas only two studies 
found no significant differences between study group and 
control group [15]. With the VR-based SG DocTraining [19] 
medical students can train to diagnose symptoms of virtual 
patients while professors can observe and assess knowledge. 
The application creates symptoms, diseases and samples in a 
machine-learning algorithm using real data but symptoms are 
mostly limited to textual depictions. In comparison to our 
approach, interaction between players is restricted to verbal 
communication. DocTraining uses GoogleCardboard-based VR, 
which limits interaction fidelity by the lack of supporting 6-DOF 
input devices. DocTraining has not been evaluated yet. 
 
III. DEVELOPMENT SETUP 
The main goal is to showcase whether and how VR 
technology can enhance medical training, that includes analysis 
of development methods, focus in medical content, creating 
appropriate technology solutions and evaluation methodology, 
as well as identification of existing hurdles that we need to 
resolve to lead to future market and product development. 
A. Interdisciplinary setup 
The publicly funded project EPICSAVE (www.epicsave.de) 
involves an interdisciplinary cross-regional consortium that 
incorporates expertise from all relevant disciplines, i.e., 
paramedic training academies, media education, VR and gaming 
technology and arts, serious game design, commercial serious 
game and simulation production and sales. Hence, collaboration 
requires 
management 
of 
interdisciplinary 
design 
and 
implementation tools that offer remote functionality. The 
remainder of this section discusses the actual workflow, presents 
chosen design tools and co-working arrangements. 
B. Overview of the iterative, interdisciplinary work-flow 
The overall workflow (see Fig. 3) is an iterative process over 
the course of a three-year project, constrained by external 
funding with an overall budget of nearly 2 Mio €. The process 
consists of a requirements analysis, followed by two iterations 
of a set of design, implementation and evaluation phases that 
lead to two subsequently developed prototypes. 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
C.  Requirements analysis and pilot study 
After starting the project in March 2016, the phase of 
requirements analysis of vocational training practice began. This 
phase involved the analysis of the target groups (trainers and 
trainees) and the analysis of the medical content. 
1) Analysis of target groups 
We created a target group survey for trainees of the two 
paramedic academies (n=31, across the two educational 
institutions). This study provided important insights for the 
development of learning and competence goals and for the user 
experience concept. As one part of the target group analysis, a 
learning style diagnostic with the Kolb Learning Style Inventory 
was carried out [17]. In our sample, a learning style based on 
active and experience-based learning style predominates (74 %). 
The results suggest that trainees prefer methods and 
technologies in which they can actively translate their 
knowledge into actions.  
2) Analysis of Gamification User Types 
We conducted an analysis of gaming preferences using the 
24-items Gamification User Types Hexad Scale [18]. The scale 
includes six user types: Philantropist, Socialiser, Free Spirit, 
Achiever, Player, and Disruptor. The reportedly predominant 
type of user are Philanthropist and Socializer, but also Achiever. 
Disruptive play is not preferred. In each case, two substantial 
parts of the participants (each about 42%) stated that they either 
(1) played regularly (several times a week or more often) or (2) 
only occasionally during the year or not at all. The preferred 
platform was surprisingly local multiplayer, which is currently 
less common than the local single-player mode, mobile games 
or networked online games, but the preference seems to reflect 
the team spirit of pre-clinical emergency medicine.   
3) Analysis of emergency medical procedures 
This analysis aimed at introducing both educational and 
technology researchers to processes and conditions of 
emergencies. The consortium arranged two on-site assessments 
at different paramedic training academies. Both events consisted 
of two training sessions with instructors, trained emergency 
paramedics, actors or simulator phantoms and medical staff (see 
Fig. 2). These runs were filmed with two 360-degree cameras to 
capture the process as well as the training environment. We then 
examined the evidence-based guidelines for treatment of 
anaphylaxis and studies that highlight common mistakes in pre-
hospital treatment and care of patients with anaphylaxis [19]. 
Based on these analyzes, we determined the potential of (D) 
technology-enhanced learning objectives, (E) defined focus of 
medical learning content, and set requirements for (F) VR 
technology and usability.  
D. Learning objectives  
We focus on three learning objectives that were discussed 
and coordinated with the educational partners:  
1. Declarative knowledge: Trainees can correctly identify, 
interpret and explain key symptoms of anaphylaxis from 
assessment using diagnostic algorithms, i.e., ABCDE-
Scheme. 
2. Procedural knowledge: Trainees can initiate technical 
diagnostics and advanced emergency medical measures.   
 
Fig. 3. The iterative work-flow showing two prototype iterations to be 
developed by an inter-disciplinary consortium. 
 
Fig. 2. 
Impressions from four training sessions on two paramedic training sites. Upper row at Akademie für Notfallmedizin der Hansestadt Hamburg (left to right):
adult phantom simulator outside a train wagon, same scene from a view of a 360° camera. child phantom inside train wagon setup and actual training session. 
Lower row at Malteser Bildungszentrum HRS in Wetzlar (from left to right): outdoor park scene with adult phantom, transport in ambulance car, indoor scene 
with actor, transport through stair case. All scenes were filmed using two 360° cameras  and additional mobile phones. 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
3. Non-technical-skills: Trainees can carry out care of the 
patient under consideration of success factors of 
collaborative teamwork.  
E. Medical learning content 
We (1) developed concrete pre-clinical case scenarios, (2) 
the feature space with patient’s symptoms, and diagnostic and 
therapeutic actions with required medical equipment, and (3) the 
setup of the training scenario.  
1) Pre-clinical case scenario 
Developing concrete cases of anaphylaxis emerged as an 
interdisciplinary challenge: While medical educationalists 
prefer algorithm of user steps and predetermined outcomes, 
game developers require flow-charts that support multiple 
outcomes depending on interactive user dynamics. Development 
costs rise with complexity of such flow-charts, e.g., with number 
of outcomes and required amount of implementation of 
graphical symptoms, and of according treatment interactivity. It 
took multiple work meetings to develop a mutual understanding 
that led to scenario flow-charts (see Fig. 5) as a general tool that 
we can now apply for developing concrete case scenarios. 
2) Medical feature space 
Table 1 shows our resulting focus of features concerning the 
virtual patient and the diagnostic/therapeutic measures that 
trainees can perform. In addition, the development of a 
pathophysiological model for virtual patients was started. Fig. 4 
depicts an example of procedurally generated visual symptoms, 
TABLE I.  
FEATURES OF THE VIRTUAL PATIENT AND INTERACTION 
OPPORTUNITIES OF TRAINEES  
Virtual Patient (clinical presentation. 
vital signs and parameters) 
Interaction opportunities of trainees 
within the VR-scenario 
x Dizziness and confusion 
x Urticaria, angioedema 
x Laryngeal edema and airway 
obstruction 
x Dyspnea, tachypnea, orthopnea, 
inspiratory and expiratory stridor, 
intercostal retractions 
x Cyanosis, mild to severe Hypo- 
or Hypertension 
x Tachycardia and tachyarrhythmia 
x Syncope 
x Postural change  
x First survey according to the 
ABCDE-scheme 
x Intramuscular application and 
inhalation of epinephrine; 
Administration of oxygen and 
intravenous infusions 
x Patient re-positioning, remove 
patients clothing 
x Monitoring of pulse oximetry, 
ECG, blood pressure, 
temperature, sugar  
x Inspection of oropharyngeal 
region, auscultation of the lungs  
 
which a trainer can adjust dynamically during training. 
Developing these symptoms was challenging, as it required 
quick feedback loops between designers, programmers and 
medical experts. We developed an online meeting environment 
that allowed all groups to participate in editing meetings. 
3) Training scenario setup and procedure 
Two trainees wearing head-mounted displays take on the 
roles of collaborating emergency paramedics. According to the 
3 phases of medical simulation training, in the first phase of 
familiarization with the VR learning environment (up to 30 
minutes), the trainees can try interaction possibilities, e.g., the 
use of diagnostic and therapeutic equipment, spatial navigation, 
and communication channels, etc. Trainees then enter a specific 
emergency scenario (training phase – 30 minutes) in which a 
virtual patient shows symptoms of severe anaphylaxis. Both 
trainees must apply systematic, guideline-based diagnostics 
(ABCDE scheme), therapy, and success factors in teamwork. An 
instructor (trainer) performs initiation and control of the 
scenario. She evaluates trainees' actions either in real-time on 
screen as well as in log-files generated for a subsequent de-
briefing session. The software offers recording of the training so 
that trainees can retrospectively assess their behavior and actions 
from different perspectives (ego perspective, bird's-eye view). 
After training, debriefing takes around 15 minutes. 
F. VR technology and usability 
The primary identified goal for selecting VR hardware and 
for developing VR software is easy integration and maintenance 
at paramedic education sites. A first key requirement is the use 
of off-the-shelf equipment. We compared different VR headsets, 
primarily Oculus Rift CR1 and HTC VICE, and selected the 
latter for the first prototype based on its advantages in 3D 
interaction, walk-in volume and support of multiple users in a 
shared tracked volume, directly mimicking a training scenario. 
Disadvantages compared to the Oculus were a lower legibility 
of writing related to lens/display design and poorer wearer 
ergonomics. A second key requirement is the optimization of 
usability in software. We analyzed more than 30 VR games for 
ways of navigation, interaction, and visualization. Most VR 
games were aquired/purchased via steam for the HTC VIVE like 
"Surgeon Simulator VR: Meet The Medic", "Job Simulator", 
"POLLEN", "Tilt Brush", "Windlands" and more recent games 
like "The Body VR: Jouney Inside a Cell" or "Vacate the Room". 
We also analysed games on the Playstaion 4 with the PSVR-
Headset like "Fantastic Contraption" and "Batman: Arkham 
VR". Being aware of the history of 3D interaction in VR with a 
large variety of methods [20] we were looking for evolving 
standards users might rely on. Our findings are that there are no 
standards, yet, so VR systems must be very careful in teaching 
how to use them. 
1) Navigation 
 The HTC VIVE tracking system offers sufficient shared 
physical space to two users. In our current scenario, the 
interaction scope is bounded to the provided physical space so 
that no other navigation approaches are necessary, besides the 
natural movements of the players. In the future, we need to allow 
single users to leave the limited virtual space, e.g., to virtually 
walk to the ambulance car to fetch additional equipment. We 
 
Fig. 4. Real-time graphics showing a healthy patient (left) besides a patient 
showing symptoms (center), as triggered using a trainer tool (right). 
Textures and mesh morphings for symptoms were precedural generated 
by using Substance Designer. 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
hence evaluate alternative continuous navigation approaches 
where the player is moving the avatar while standing still. One 
major side effect is motion sickness, which can be mitigated by 
using teleport, visual extensions, reducing speed or avoiding 
strong acceleration [21]. Teleport navigation may reduce motion 
sickness, with disadvantages in a less immersive movement, or 
possible disorientation, and optimization potential in teleport 
transitions, e.g., by showing animated zooms, fade-to-black, or 
via orientation volumes. De-coupling the volumes for one player 
in a co-located multi-user volume requires visualization of the 
current physical and/or virtual positions of the other user/player. 
Another future challenge is how to re-match virtual and physical 
volumes after de-coupling through user redirection. 
2) Interaction 
A recent list of VR interaction techniques can be found in 
[22] that can be roughly divided into virtual hand and virtual 
pointer techniques. The virtual hand can select an object by 
coming close to it, a trigger pairs orientation of the hand with 
that of the object. The pointer is a line reaching out from a user’s 
hand position or controlled via gaze direction. It can be used to 
reach distant objects. We propose to use virtual hand in general 
for virtual content and the pointer for selecting menus. 
To model the virtual scenario as realistic as possible, we 
implemented medical cases and instruments in their real shape 
and proportion. Users can pick items by putting the virtual hand 
into a case and navigating through a superimposed virtual menu 
(see Fig. 6). One main challenge in a learning environment is 
that such menus can prime a user about what she may do or not. 
However, real paramedics cannot rely on such mental support. 
A second challenge is that the VIVE controller requires a hand 
posture that does not match with more fine grain interactions, 
such as injecting a syringe. Haptics are clearly missing here. 
Third, items are dropped occasionally and thus fall to the 
ground, mostly for reasons of erroneous interaction. A good 
solution is to let items float for 3-5 seconds until being dropped 
slowly. This allows a user to re-grab them. 
We also evaluated additional inventory design concepts in 
[23] where paramedics can store, access and manage their 
medical instruments via a metaphoric belt or an abstract menu-
based tablet. In an evaluation, 24 paramedic trainees rated utility 
of both concepts rather high. In a multi-user scenario, seeing 
what items another user currently owns can support context 
awareness. 
3) Visualization 
Volante et al. [24] compared the effects of three different 
visualisations of a virtual patient (realistic vs. cartoon like vs. 
sketch like) on potential caregivers in a clinical patient scenario, 
indicating that visually less realistic avatars can evoke strong 
feelings. We therefore apply a moderately detailed realistic art 
style in general. Visual body symptoms—realized through 
procedural texture blending—are implemented as realistic as 
possible to support fine grain severity diagnosis (see Fig. 4). 
Environmental objects or medical instruments are designed in a 
clean, functional manner to foster focus on training procedures 
(see Fig. 6).  
The current visualisation of player avatars is limited to the 
head and to the hands, i.e., the only limbs tracked by HTC VIVE 
Lighthouse system. The other body parts can be tracked using 
additional trackers and/or by applying inverse kinematics. First 
results show that attaching additional trackers can be a 
cumbersome procedure and that bad signal and virtual body 
modelling often leads to effects that break immersion. 
Especially, prone body poses, which occur quite often in our 
scenario, cause interbody-occlusion which damps the signal 
significantly. Another attempt was to connect head and hands of 
the playing avatars with elastic tubes to imply cohesiveness. 
User reactions are rather negative, pointing out unnatural effects 
and a request for occlusion through body mass. In summary, 
effective multi-user VR requires more elaborate solutions for 
this problem space. 
4) Multi-user tasks 
Our current setting involves three users: Two trainees 
encounter each other as treating paramedics and one trainer (a 
medical specialist) can operate on the scenario by influencing 
the patient’s symptoms. The paramedics work as a team, 
communicating, supervising and helping each other by giving 
hints and handing out instruments. The trainer controls patient 
health and physiology or provides feedback on anamnesis. We 
identified emotionalization as a key potential in multi-user 
situations. A lack of gaze animation or facial mimics of 
paramedics and the patient becomes obvious once you use the 
simulation. However, a first evaluation on the impact of eye 
tracking on co-located VR situation reported no difference in 
perceived quality of conversation between random gaze and eye-
tracking-based gaze animations [25].   
G. Development Process 
After the pilot studies and the concept phase where we found 
focus in medical procedures and created cases, we first 
established a virtual training environment as common meeting 
place. The technology is based on TriCAT Spaces commercial 
remote meeting software. It allows multiple users to enter a 
Fig. 5. Basic diagnostic and therapeutic measures according to the ABCDE-
Scheme, excerpt for A = Airway. The graph shows a concrete 
paramedical case as interactive scenario, i.e.,how user reactions lead to 
different outcomes. 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
shared VE using desktop PCs or VR platforms. We used the 
training environment also as a virtual meeting place to evaluate 
the latest development and for interdisciplinary Scrum meetings.  
IV. EVALUATION 
A. Setup and preparation 
Based on the learning objectives and on models of learning 
in VR [26], we conducted a user study on the simulation 
prototype. As metrics, we collected data on presence experience 
and usability. We tested training design and outcomes with 24 
trainees (♂ = 19, ♀ = 5, mean age = 23.3) in both paramedic-
training institutions. All 24 trainees completed the three phases 
(familiarization, scenario, and debriefing) as described above. 
As preparation, we had previously trained the trainers in three 
sessions, introducing them to the current state of prototype 
development and the use of the VR as a training method.  
B. Hypothesis and metrics  
1) Hypothesis 
We hypothesize, that high usability (interaction experience) 
contributes to a high presence experience (as a precondition of 
learning experience). Parameters that were evaluated as part of 
a bivariate correlation analyses were the variables (mean scores) 
of the usability and the presence questionnaire. The learning 
experience was analyzed with descriptive statistic, using the 
Training Evaluation Inventory. Furthermore, focused interviews 
were conducted and analyzed.  
2) Presence  
We obtained presence measures from subjective rating 
scales, using the Igroup Presence Questionnaire (IPQ). IPQ 
measures the sense of presence experienced in a VE. It has three 
subscales with a maximum mean score of 6 and one additional 
general item. The three subscales are: (1) Spatial Presence - the 
sense of being physically present in the VE. (2) Involvement - 
measuring the attention devoted to the VE and the involvement 
experienced, and (3) Experienced Realism - measuring the 
subjective experience of realism in the VE. The additional 
general item assesses the general "sense of being there", with an 
especially strong loading on spatial presence [27]. 
3) Usability  
The System Usability Scale (SUS) [28] is a simple and 
technology-independent questionnaire to assess usability of a 
system. The SUS is an established method for the quantitative 
analysis of the usability. It includes 10 Likert-scaled items.  
4) Learning Experience  
The TEI (Training Evaluation Inventory) is an instrument for 
evaluating learning experience in trainings. The questionnaire 
can be included in both, formative and summative, training 
evaluation. The ten scales of the TEI cover training-relevant 
outcome dimensions as well as aspects of training design [29]. 
Kirkpatrick's (1998) influential hierarchical evaluation model 
with the four levels "reactions", "learning", "behavior" and 
"results" formed the basis for the conception of the outcome 
dimensions subscale (max. dimensions score value M = 5.0). 
The training design focuses on the aspects of a training that 
support the participants in learning and in transfer of what they 
have learned into their daily work. The dimensions of the 
training design are based on Merrill's principles of instructional 
learning (2002) (max. dimension score value M = 5). 
 
C. Results 
1) Presence Experience  
Table II shows the sample mean scores for the general item 
and the three subscales. The scores indicate a high sense of 
overall and spatial presence, while experienced involvement and 
realism scored on the medium range. 
To better interpret the subscale values, we conducted 
focused interviews with the 24 participants. Results indicate that 
high presence scores emerge early in the VR-training. However, 
there are still experiences of “breaks in presence” (BIP) as 
participants are bound to two ways of communication: (1) with 
the team partner (trainee) using headphones ("in the virtual 
world") and (2) directly with the instructor ("in the real world"). 
Spatial navigation in VE is also described as difficult. In addition 
to the interference caused by the headset-cables, the incomplete 
graphical representation of co-worker’s avatar is reportedly 
irritating. Since the strength of multi-user VR simulation 
training lies in team-based work or the promotion of so-called 
non-technical skills, these aspects (interaction, communication 
and navigation) should face significant improvement. 
2) Usability/Interaction Experience  
The total mean score (M) of the SUS was 63.95 (SD 12.96; 
R 32.5–87.5). Is such a score sufficient to say, that the prototype 
 
Fig. 6. A virtual patient can be examined, e.g., by opening the mouth or taking the pulse (left and center-left). The environment shows the patient in context with 
medical instruments in use and a floating overlay menu (center-right); the available medical instruments as implemented (right). 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
TABLE II.  
SAMPLE MEAN SCORE FOR IPQ-SUBSCALES 
IPQ-
Subscale 
Overall 
experienced 
presence 
Spatial 
presence 
Involvement 
Experienced 
Realism 
M 
5.04 
4.45 
3.45 
3.16 
SD 
1.09 
0.99 
1.5 
1.01 
TABLE III.  
SUS PERCENTAGE DISTRIBUTION ON THE GRADE SCALE 
Grade 
Scale 
F (0-60) 
D (60-70) 
C (70-80) 
B (80-90)  
A (>90) 
% 
29.2% 
37.5% 
25.0% 
8.3% 
0.0% 
is usable? We examined the individual users' SUS scale values 
regarding their statements in the focused interviews. Users with 
scale values in the lower quartile (R 32.5–50.0) particularly had 
problems with real body movement, caused by headset cables 
and by wearing the headset. We found medium to very strong 
correlations between the SUS scale values and single subscales 
of IPQ-presence questionnaire (r=.59 [SUS/Overall experienced 
presence], 
r=.63 
[SUS/Spatial 
presence], 
r=.48 
[SUS/Involvement], r=.73 [SUS/Experienced Realism]. Table 
III shows the percentage distribution of single scale scores based 
on the grade scale, which Bangor et al. propose as interpretation 
aid [30]. 
3) Learning Experience 
The results of the TEI-Questionnaire show above-average 
values for all subscales, respectively for the two dimensions 
(Training Outcome Dimensions: M = 3.8 [SD 0.37]; Training 
Design Dimensions: M = 3.4 [SD 0.36]).  The scale values of 
both dimensions show a very strong correlation (r=.80). In our 
study, the training design values are slightly lower than the 
training outcome values. The training design dimensions serve 
as antecedents of training outcomes.  
4) User comments 
Most users commented enthusiastically about their experiences, 
how deep they felt inside the scenario, e.g., “like with no other 
training medium ever”. Multiple users experienced warm 
temperatures reportedly due to the summer lake scenario of the 
session, albeit the training took place in cold October. Most 
reported usability issues related to the wired connection of the 
head-set, the visual quality of the head-set, or the sound issues, 
communicating with trainees and trainers using different media. 
 
V. DISCUSSION 
The correlations formulated in the hypothesis can be 
confirmed by the results of the user study: The noted usability 
issues (see above) contribute to “breaks in presence” experience 
and extraneous cognitive load (split attention effect). This in turn 
seems to limit results to above-average values in the perceived 
learning effectiveness. Furthermore, the results of the TEI 
questionnaire also show that trainers and trainees must be even 
more actively involved in the ongoing development and 
implementation process. However, the relatively high scores for 
the training outcome dimensions shows that trainees rate the 
VR-simulation as a training method with a great impact on the 
development of professional skills. 
TABLE IV.  
VR DESIGN ISSUES FOR TRAINING SIMULATIONS 
Area 
Issue 
Proposed solutions 
Navigation 
In general 
Use tracked volume for focused 
scenario 
 
Cyber sickness 
Teleport, visual extensions, slow 
speed 
 
De-/re-coupling of 
sharing virtual/ 
real space 
Visualization of partner avatar 
location in real volume 
Interaction 
In general 
Virtual hand for items, virtual pointer 
for menus 
 
Context-aware 
inventory 
Virtual belt 
 
Menus bias user 
decision making 
Pure audio interfaces  
 
Fine interactions 
Different input devices or gloves? 
 
Missing haptics 
Visualize haptic experiences 
 
Lost dropped 
items 
Let loose items temporarily float in air 
Visualization 
Realism vs. comic 
visuals 
Not highest production value, but 
medical details need to be adequate 
 
Multi-user 
awareness 
Full-body tracking and visualization, 
abstract solutions, ghosts 
Multi-user 
Emotionalization 
Use eye tracking for gaze animation, 
real-time animations of mimics 
 
Communication 
Support different channels between 
trainees, and with trainers 
We identified several usability issues. We received no 
negative comments on the medical content. The visual style 
(realistic but not too detailed) and quality of symptoms worked 
out fine. Also, from a current technical point of view, we cannot 
sufficiently provide a full simulation of body health and 
physiological reaction, offer all kinds of fine-motor interactions 
at all body points, simulate haptics (e.g., cables, small 
environments, organic elasticity). But having a trainer control 
physiology and health as reactions to the trainees’ actions in a 
Wizard-of-Oz-like situation appears a good solution. 
The described interdisciplinary development process has 
strongly contributed to these results. The implemented Scrum 
process was supportive in that it provided regular meetings. But 
we could not maintain full agile methodology in an 
interdisciplinary setup. In the future, we will switch to longer 
development 
phases 
(>4 
weeks) 
with 
active 
direct 
communication channels between developers and technical 
experts, with more sub-milestones throughout the process. 
During the preceding development phase, we identified a set of 
open issues of VR technology, that needs to be addressed in 
research and development. Table IV lists these issues. 
 
VI. CONCLUSIONS AND FUTURE WORK 
This paper showcases the interdisciplinary and iterative 
process of developing EPICSAVE, a virtual reality training 
simulation for paramedics. The results of an evaluation show 
that a positive learning experience depends on a high presence 
experience which gains from high interactivity and VR usability. 
Especially usability in VR is an issue as there are no standards, 
yet. Such rough factors in our VR-prototype, which lead to 
“breaks in presence” experience and cognitive load (e.g., 
communication and navigation in VE, wired head-sets). Our 
development case offers a detailed report how close 
interdisciplinary collaboration and the iterative workflow 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
contribute to medical focus and reasonable production 
performance creating novel VR-based training media that also 
incorporate new didactic concepts. We identified a list of open 
design issues that we will tackle in future work and strongly 
encourage other researchers to join us doing so. In general, to 
contribute to a better training outcome, we must improve 
integration of VR in training facilities and learning impact for 
more target groups. Our next approach will examine how serious 
gaming can contribute to motivational aspects and learning 
effect beyond the simulation level demonstrated here. 
ACKNOWLEDGMENT 
This work was carried out within the project EPICSAVE funded by the 
German Federal Ministry of Education and Research (BMBF) and by the 
European Union, European Social Fonds (ESF), FKZ 01PD15004). Besides the 
authors, the following people have contributed to this work: Markus Herkersdorf, 
Konstantin Wegner, Frederik Schneider, Markus Neuberger, Andreas Franke, 
Claus Kemp, Johannes Pranghofer, Liubov Gorodilova, Eva Blum, Benjamin 
Roth, Leonard Flock, Sven Seele, Helmut Buhler and Rainer Herpers. 
REFERENCES 
[1] B. Urban, M. Lazarovici, B. Sandmeyer. “Simulation in medicine – 
inpatient simulation”, Simulation in der Notfallmedizin – Stationäre 
Simulation, M. St. Pierre, G. Breuer, (Eds.), “Simulation in medicine”, 
Simulation in der Medizin. Springer, Berlin Heidelberg, 2013, pp. 231–
248. 
[2] F. E. R. Simons, M. Ebisawa, M. Sanches-Borges, B. Y. Thong, M. 
Worm, L. Kase Tanno,  et al., “2015 update of the evidence base: World 
Allergy 
Organization 
anaphylaxis 
guidelines”, 
World 
Allergy 
Organization Journal 2015; 32 (8), pp. 1–16. 
[3] O. Meyer, “Simulators don´t teach – Process of Learning and Simulation”, 
M. St. Pierre, G. Breuer, (Eds.), “Simulation in medicine”, Simulation in 
der Medizin. Springer, Berlin Heidelberg, 2013, pp. 55–70. 
[4] S. De Ribaupierre, B. Kapralos, F. Haji, E. Stroulia, A. Dubrowski, R. 
Eagleson, “Healthcare Training Enhancement Through Virtual Reality 
and Serious Games”, M. Ma, L. C. Jain, P. Anderson, (Eds), “Virtual, 
Augmented Reality and Serious Games for Healthcare 1”, Springer, 
Berlin Heidelberg, 2014, pp. 9–28. 
[5] R. Dörner, S. Göbel, W. Effelsberg, J. Wiemeyer (Eds.), “Serious Games. 
Foundations, Concepts and Practice“, Springer International Publishing, 
Switzerland, 2016. 
[6] M. Kerres, “Media Didactics: Design and Development media-supported 
Learning”, 
Mediendidaktik: 
Konzeption 
und 
Entwicklung 
mediengestützter 
Lernangebote, 
Oldenbourg 
Wissenschaftsverlag, 
München, 2013, pp. 241–255.      
[7] R. Ghanbarzadeh, A. H. Ghapanchi, M. Blumenstein, A. Talaei-Khoei, 
“A Decade of Research on the Use of Three-Dimensional Virtual Worlds 
in Health Care: A Systematic Literature Review”, Journal of Medical 
Internet Research 2014, 16 (2), e47, pp. 1–20. 
[8] A. Correia, B. Fonseca, H. Paredes, P. Martins, L. Morgando, “Computer-
Simulated 3D Virtual Environments in Collaborative Learning and 
Training: Meta-Review, Refinement, and Roadmap”, Y. Sivan, (Eds.), 
“Handbook on 3D3C Platforms, Progress in IS”, Springer International 
Publishing, Switzerland, 2016, pp. 403–440. 
[9] F. Consorti, R. Mancuso, M. Nocioni, A. Piccolo, “Effecacy of virtual 
patients in medical education: A meta-analysis of randomized studies”, 
Computers & Education 2012, 59 (3), pp. 1001–1008. 
[10] Z. Merchant, E. T. Goetz, L. Cifuentes, W. Keeney-Kennicutt, T. J. Davis, 
“Effectiveness of virtual reality-based instruction on students` learning 
outcomes in K-12 and higher education: A meta-analysis”, Computers & 
Education, 2014, 70, pp. 29–40. 
[11] C. D.  Flores, P. Barros. S. Cazella, M. R. Bez, "Leveraging the learning 
process in health through clinical cases simulator." Serious Games and 
Applications for Health (SeGAH), 2013 IEEE 2nd International 
Conference on. IEEE, 2013. 
[12] M. A. Zielke, D. Zakhidov, G. Hardee, L. Evans, S. Lenox, N. Orr, D. 
Fino, G. Mathialagan, "Developing Virtual Patients with VR/AR for a 
natural user interface in medical teaching." Serious Games and 
Applications for Health (SeGAH), 2017 IEEE 5th International 
Conference on. IEEE, 2017. 
[13] E. A. Boyle, T. Hainey, T. M. Connolly, G. Gray, J. Earp, M. Ott, et al., 
“An update to the systematic literature review of empirical evidence of 
the impacts and outcomes of computer games and serious games”, 
Computers & Education, 2016, 64 (2), pp. 178–192 
[14] P. Wouters, C. van Nimwegen, H. van Oostendorp, E. E. van der Spek, 
“A Meta-Analysis of the Cognitive and Motivational Effects of Serious 
Games”, Journal of Educational Psychology, 2013, 105 (2), pp. 249–265. 
[15] R. Wang, S. Jr. DeMaria, A. Goldberg, D. Katz, “A Systematic Review 
of Serious Games in Training Health Care Professionals”, Simul Healthc, 
2016, 11 (1), pp. 41–51. 
[16] R. M. de Lima, A. de Medeiros Santos, F. M. M. Neto, A. F. de Sousa 
Neto, F. C. P. Leão, F. T. de Macedo, A. M. de Paula Canuto, "A 3D 
Serious Game for Medical Students Training in Clinical Cases" Serious 
Games and Applications for Health (SeGAH), 2016 IEEE International 
Conference on. IEEE, 2016. 
[17] D. Kolb, A. Kolb; “The Kolb Learning Style Inventory 4.0: Guide to 
Theory, Psychometrics, Research & Ablications”, 2013. 
[18] G. F. Tondello, R. R. Wehbe, L. Diamond, M. Busch, A. Marczewski, L. 
E. Nacke, “The Gamification User Types Hexad Scale“, CHI PLAY ’16 
Proceedings of the 2016 Annual Symposium on Computer-Human 
Interaction in Play, 2016, pp. 229–243. 
[19] D. A. Sclar, P. L. Lieberman, “Anaphylaxis: Underdiagnosed, 
Underreportet, and Undertreated“, The American Journal of Medicine 
2014, 127 (1), pp. S1–S5. 
[20] D. A. Bowman, E. Kruijff, J. J. LaViola, I. Poupyrev, “3D User 
Interfaces–Theory and Practice“, Pearson Education, Boston, 2005. 
[21] M.L. Ibáñez,  F. Peinado, “Walking in VR: Measuring Presence and 
Simulator Sickness in First-Person Virtual Reality Games, 2016, Palmieri, 
O., Full Speed Flying in VR! The R&D behind Eagle Flight, 2016, Oculus 
VR, Oculus Best Practices, 2016 
[22] F. Argelaguet, C. Andujar, “A Survey of 3D Object Selection Techniques 
for Virtual Environments”, Computers & Graphics, 2013, 37 (3), pp. 121–
136. 
[23] K. Wegner, S. Seele, H. Buhler, S. Misztal, R. Herpers, J. Schild, 2017. 
“Comparison of Two Inventory Design Concepts in a Collaborative 
Virtual Reality Serious Game”. In CHI PLAY '17 Extended Abstracts. 
ACM, New York, NY, USA, 2017, pp. 323–329. 
[24] M. Volante, S. V. Babu, H. Chaturvedi, N. Newsome, E. Ebrahimi, T. 
Roy, S. B. Daily, and T. Fasolino. "Effects of virtual human appearance 
fidelity on emotion contagion in affective inter-personal simulations." 
IEEE transactions on visualization and computer graphics 22.4, 2016, pp. 
1326–1335. 
[25] S. Seele, S. Misztal, H. Buhler, R.Herpers, J. Schild, “Here's Looking At 
You Anyway!: How Important is Realistic Gaze Behavior in Co-located 
Social Virtual Reality Games?”, Proceedings of the Annual Symposium 
on Computer-Human Interaction in Play CHI PLAY '17, ACM, 2017, pp. 
531–540.  
[26] M. C. Salzman, C. Dede, R. B. Loftin, J. Chen, “A Model for 
Understanding How Virtual Reality Aids Complex Conceptual 
Learning”, Presence 1999, 8 (3), pp. 293–316. 
[27] T. W. Schubert, “The sense of presence in virtual environments: A three-
component scale measuring spatial presence, involement, and realness”, 
Zeitschrift für Medienpsychologie, 2003, 15 (2), pp. 69–71. 
[28] J. Broke, “SUS – A quick and dirty usability scale”, Usabitlity Evaluation 
in 
Industry, 
Taylor 
and 
Francis, 
London, 
1986, 
http://www.usabilitynet.org/trump/documents/Suschapt.doc 
[29] S. Ritzmann, V. Hagemann, A. Kluge, “The Training Evaluation 
Inventory (TEI) – Evaluation of Training Design and Measurement of 
Training Outcomes for Predicting Training Success”, Vocations and 
Learning 2014, 7 (1), pp. 41–73. 
[30] A. Bangor, P. Kortum, J. Miller, “Determining What Individual SUS 
Scores Mean: Adding an Adjective Rating Scale”, Journal of Usability 
Studies 2009, 4 (3), pp. 114–123. 
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:18 UTC from IEEE Xplore.  Restrictions apply. 
