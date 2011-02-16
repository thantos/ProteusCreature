using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProteusCreature
{
    class Combat
    {
        /** 
         * TODO: Add creature to battle method
         *       Add team to battle method, maybe
         *          We could also just have creatures know who they are enemies with...
         *              Seomthing to think about
         **/


        /**
         * We have a bunch of things to do with this, this is what will control combat on the inside at least.
         * A few choices are out there, Such as whether or not to impliments teams, or to have each creature know allies n such.
         * We can have something like enemies, allies, and neutral, fight enemies, ignore neutrals, help allies.
         * for now... proteus shouldn't have allies, unless we let other animals jumpinto the battle to help. that'd be kinda cool.
         **/

        List<creature> enemies{get;set;}


    }
}
